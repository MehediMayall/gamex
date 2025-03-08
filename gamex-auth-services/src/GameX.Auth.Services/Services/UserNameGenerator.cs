namespace gamex.Auth.Services;

public sealed class UserNameGenerator: IUserNameGenerator{

   string[] adjectives = {
            "Cool", "Fast", "Bright", "Smart", "Bold", "Mighty", "Swift", "Shiny", "Clever", "Brave",
            "Quiet", "Stormy", "Happy", "Funky", "Silent", "Witty", "Lively", "Quirky", "Daring", "Zany",
            "Feisty", "Gentle", "Kind", "Loyal", "Noble", "Playful", "Radiant", "Silly", "Trusty", "Valiant",
            "Weird", "Eager", "Fancy", "Gleaming", "Hopeful", "Jolly", "Keen", "Lucky", "Merry", "Nifty",
            "Peppy", "Quick", "Rugged", "Steady", "Tough", "Unique", "Vivid", "Wild", "Zesty", "Cheery",
            "Bouncy", "Careful", "Dashing", "Earnest", "Fearless", "Gutsy", "Heroic", "Jazzy", "Kindhearted", "Luminous",
            "Magical", "Nimble", "Optimistic", "Powerful", "Quiet", "Resilient", "Sassy", "Thoughtful", "Upbeat", "Whimsical",
            "Youthful", "Zealous", "Adventurous", "Blazing", "Cheerful", "Dynamic", "Energetic", "Fluffy", "Gallant", "Hearty",
            "Inventive", "Jubilant", "Kooky", "Lively", "Majestic", "Nurturing", "Outgoing", "Playful", "Reliable", "Sturdy",
            "Timely", "Victorious", "Witty", "Xenial", "Yearning", "Zephyr", "Alert", "Brilliant", "Charming", "Dependable",
            "Excited", "Fancy", "Graceful", "Harmonious", "Imaginative", "Jovial", "Knowledgeable", "Likable", "Mysterious", "Noteworthy",
            "Optimistic", "Polished", "Resourceful", "Savvy", "Tactful", "Unstoppable", "Versatile", "Warmhearted", "Youthful", "Zippy",
            "Agile", "Blissful", "Courageous", "Delightful", "Energetic", "Fearless", "Glorious", "Hopeful", "Incredible", "Joyful",
            "Kindly", "Lovable", "Magnetic", "Noble", "Outstanding", "Perky", "Radiant", "Sparkly", "Thrilling", "Uplifting",
            "Vigorous", "Wandering", "Zesty", "Adaptable", "Boisterous", "Chivalrous", "Diligent", "Enthusiastic", "Fearsome", "Gracious",
            "Helpful", "Inquisitive", "Jumpy", "Kinetic", "Lustrous", "Magnificent", "Obedient", "Practical", "Questioning", "Respectful",
            "Skillful", "Tenacious", "Unflinching", "Valorous", "Whimsical", "Xenodochial", "Yielding", "Zealful", "Adept", "Bold",
            "Calm", "Daring", "Effervescent", "Fervent", "Gentle", "Humble", "Innovative", "Jocular", "Keen", "Lively",
            "Mindful", "Nimble", "Observant", "Pleasant", "Quiet", "Restful", "Steadfast", "Thriving", "Upright", "Versed",
            "Xtraordinary", "Youthful", "Zappy"
        };

    string[] nouns = {
            "Tiger", "Eagle", "Panther", "Wizard", "Knight", "Phoenix", "Comet", "Blaze", "Arrow", "Falcon",
            "Raven", "Shadow", "Hunter", "Voyager", "Seeker", "Pioneer", "Guardian", "Druid", "Nomad", "Scout",
            "Lion", "Dragon", "Bear", "Wolf", "Shark", "Sword", "Shield", "Hammer", "Storm", "Fire",
            "Ice", "Mountain", "River", "Forest", "Ocean", "Thunder", "Lightning", "Glacier", "Meteor", "Galaxy",
            "Star", "Moon", "Sun", "Planet", "Cosmos", "Nebula", "Orbit", "Comet", "Asteroid", "Quasar",
            "Tornado", "Hurricane", "Cyclone", "Volcano", "Earthquake", "Avalanche", "Blizzard", "Whirlwind", "Twister", "Stormcloud",
            "Pebble", "Boulder", "Canyon", "Valley", "Plain", "Plateau", "Desert", "Oasis", "Jungle", "Savannah",
            "Tundra", "Island", "Peninsula", "Cape", "Harbor", "Lagoon", "Bay", "Sea", "Ocean", "Wave",
            "Cliff", "Gorge", "Fjord", "Ridge", "Summit", "Peak", "Range", "Slope", "Hill", "Meadow",
            "Grove", "Wood", "Forest", "Jungle", "Thicket", "Swamp", "Marsh", "Bog", "Fen", "Pond",
            "Lake", "River", "Stream", "Creek", "Brook", "Spring", "Waterfall", "Cascade", "Fountain", "Glade",
            "Glen", "Dale", "Valley", "Field", "Pasture", "Prairie", "Steppe", "Heath", "Moor", "Flatland",
            "Savannah", "Plain", "Plateau", "Desert", "Dune", "Oasis", "Canyon", "Gorge", "Chasm", "Ravine",
            "Abyss", "Pit", "Cave", "Cavern", "Tunnel", "Mine", "Shaft", "Quarry", "Cliff", "Bluff",
            "Promontory", "Peninsula", "Cape", "Point", "Bay", "Cove", "Inlet", "Harbor", "Port", "Dock",
            "Pier", "Marina", "Jetty", "Breakwater", "Seawall", "Beacon", "Lighthouse", "Tower", "Fort", "Castle",
            "Keep", "Citadel", "Bastion", "Garrison", "Stronghold", "Fortress", "Palace", "Mansion", "Villa", "Chateau",
            "Manor", "Hall", "House", "Cottage", "Cabin", "Shack", "Hut", "Tent", "Shelter", "Camp",
            "Village", "Town", "City", "Metropolis", "Capital", "Settlement", "Outpost", "Colony", "Frontier", "Kingdom",
            "Empire", "Realm", "Domain", "Province", "Territory", "Region", "Nation", "Country", "Continent", "World",
            "Earth", "Planet", "Moon", "Sun", "Star", "Galaxy", "Universe", "Cosmos", "Void", "Abyss",
            "Infinity", "Eternity", "Destiny", "Fate", "Fortune", "Luck", "Chance", "Providence", "Kismet", "Serendipity",
            "Adventure", "Quest", "Journey", "Odyssey", "Expedition", "Voyage", "Pilgrimage", "Mission", "Crusade", "Campaign",
            "Battle", "War", "Conflict", "Skirmish", "Duel", "Contest", "Tournament", "Challenge", "Trial", "Test",
            "Victory", "Triumph", "Conquest", "Defeat", "Loss", "Failure", "Glory", "Honor", "Pride", "Courage",
            
        };

    public string GetName() {

        Random random = new Random();

        string adjective = adjectives[random.Next(adjectives.Length)];
        string noun = nouns[random.Next(nouns.Length)];
        int number = random.Next(10, 99);

        return $"{adjective}{noun}{number}";
    }
}
