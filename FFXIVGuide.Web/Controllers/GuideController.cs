using FFXIVGuide.Web.Models.Guide;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FFXIVGuide.Web.Controllers;

public class GuideController : Controller
{
    public IActionResult Index()
    {
        var result = CreateFakeIndexViewModel();

        return View(result);
    }

    private static IndexViewModel CreateFakeIndexViewModel()
    {
        var encounters = new List<EncounterModel>
        {
            new EncounterModel()
            {
                Id = 1,
                Name = "Chopper",
                Notes = new List<string>()
            {
                "When he does circle AoE Tank or Melee DPS can stun him.",
                "Paralyze debuff can be cleansed by healer with Ensuna."
            }
            },
            new EncounterModel()
            {
                Id = 2,
                Name = "Captain Madison",
                Notes = new List<string>()
            {
                "He will run away at about 50% health."
            }
            },
            new EncounterModel()
            {
                Id = 3,
                Name = "Captian Madison",
                Notes = new List<string>()
            {
                "He will summon a pack of wolves around 50% health.",
                "He will run away at about 25% health."
            }
            },
            new EncounterModel()
            {
                Id = 4,
                Name = "Denn the Orcatoothed",
                Notes = new List<string>()
            {
                "Just focus on the boss, ignoring the bubbling water.",
                "DPS can use Limit Break at 50% health."
            }
            }
        };

        var rouletteTypeList = new Dictionary<int, string>
        {
            { 1, "Expert" },
            { 2, "Level 50/60/70/80" },
            { 3, "Main Scenario" },
            { 4, "Trials" },
            { 5, "Alliance Raid" }
        };

        var instanceList = new Dictionary<int, string>()
        {
            { 1, "Satasha" },
            { 2, "Copperbell Mines" },
            { 3, "Tam Tara Deepcroft" },
            { 4, "Some other dungeon" },
            { 5, "This is not a selection" }
        };

        return new IndexViewModel()
        {
            Sidebar = new SidebarViewModel()
            {
                InstanceList = instanceList,
                RouletteTypeList = rouletteTypeList
            },
            Instance = new InstanceDetailModel()
            {
                Id = 1,
                Name = "Sastasha",
                ImageUrl = "/images/112001_hr1.png",
                Encounters = encounters
            }
        };
    }
}
