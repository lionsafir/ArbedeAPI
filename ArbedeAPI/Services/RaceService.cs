using ArbedeAPI.DTOs;
using ArbedeAPI.Repositories;
using ArbedeAPI.Services;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RaceService : IRaceService
{
    private readonly IUserRepository _userRepository;
    private readonly IRaceRepository _raceRepository;
    private readonly IUnitRepository _unitRepository;

    private readonly Dictionary<string, List<string>> _raceStartingUnits = new()
    {
        { "Elf", new List<string> { "Warrior", "Warrior2" } },
        { "Ork", new List<string> { "Warrior", "Berserker" } },
        { "Human", new List<string> { "footman", "priest" } }
    };

    public RaceService(
        IUserRepository userRepository,
        IRaceRepository raceRepository,
        IUnitRepository unitRepository)
    {
        _userRepository = userRepository;
        _raceRepository = raceRepository;
        _unitRepository = unitRepository;
    }

    public async Task<(bool Success, string Message, object? Data)> SelectRaceAsync(string userId, string raceName)
    {
        try
        {
            if (!await _userRepository.UserExistsAsync(userId))
                return (false, "Kullanıcı bulunamadı.", null);

            if (await _userRepository.UserHasRaceAsync(userId))
                return (false, "Kullanıcı zaten bir ırk seçmiş.", null);

            if (!_raceStartingUnits.TryGetValue(raceName, out var startingUnits))
                return (false, $"Geçersiz ırk: {raceName}", null);

            var timestamp = Timestamp.GetCurrentTimestamp();
            await _userRepository.SetUserRaceAsync(userId, raceName, timestamp);

            int successCount = 0;

            foreach (var unitType in startingUnits)
            {
                try
                {
                    var stats = await _raceRepository.GetUnitStatsAsync(raceName, unitType);
                    var points = await _raceRepository.GetUnitPointsAsync(raceName, unitType);

                    if (stats == null || points == null)
                        continue;

                    string unitName = $"{raceName} {unitType}";
                    await _unitRepository.AddUnitToUserAsync(userId, unitName, raceName, stats, points);
                    successCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Birim eklenirken hata oluştu: {unitType}, Hata: {ex.Message}");
                }
            }

            return (true, $"{raceName} ırkı ve {successCount} birim başarıyla eklendi.", new
            {
                Race = raceName,
                UnitCount = successCount,
                Units = startingUnits
            });
        }
        catch (Exception ex)
        {
            return (false, $"Race seçimi sırasında hata: {ex.Message}", null);
        }
    }

    public async Task<RaceCheckResponseDto> CheckUserRaceAsync(string userId)
    {
        var data = await _userRepository.GetUserRawDataAsync(userId);
        if (data == null)
        {
            return new RaceCheckResponseDto
            {
                HasRace = false,
                ShouldRedirectToRaceSelection = true,
                Races = new List<RaceDto>()
            };
        }

        bool hasRace = data.ContainsKey("race");
        var races = new List<RaceDto>();

        if (hasRace)
        {
            string raceName = data["race"].ToString();
            Timestamp selectedAt = data.ContainsKey("raceSelectedAt")
                ? (Timestamp)data["raceSelectedAt"]
                : Timestamp.GetCurrentTimestamp();

            races.Add(new RaceDto
            {
                Id = raceName,
                Name = raceName,
                Description = GetRaceDescription(raceName),
                SelectedAt = selectedAt.ToDateTime()
            });
        }

        return new RaceCheckResponseDto
        {
            HasRace = hasRace,
            ShouldRedirectToRaceSelection = !hasRace,
            Races = races
        };
    }

    private string GetRaceDescription(string raceName)
    {
        var raceDescriptions = new Dictionary<string, string>
        {
            { "Human", "İnsanlar dengeli ve gelişime açıktır." },
            { "Ork", "Orklar güçlü ve dayanıklıdır." },
            { "Elf", "Elfler çevik ve sihir kullanımında ustadır." }
        };

        return raceDescriptions.TryGetValue(raceName, out var desc) ? desc : "";
    }
}
