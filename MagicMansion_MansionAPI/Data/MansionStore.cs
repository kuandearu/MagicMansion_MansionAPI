using MagicMansion_MansionAPI.DTO;

namespace MagicMansion_MansionAPI.Data
{
    public static class MansionStore
    {
        public static List<MansionDTO> mansionDTO = new List<MansionDTO>
        {
            new MansionDTO {Id = 1, Name = "Beach Mansion", Description = "Beautiful Sunny View"},
            new MansionDTO {Id = 2, Name = "City Mansion", Description ="Eccentric and fill with life View"}
        };

    }

}
