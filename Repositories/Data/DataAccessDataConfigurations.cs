using Infrastructure.Configurations;

namespace Infrastructure.Data
{
    public sealed class DataAccessDataConfigurations
    {
        public static DataAccessDataConfigurations Instance { get; private set; } = new DataAccessDataConfigurations();

        private DataAccessDataConfigurations()
        {
        }

        public HashSet<dynamic> Configurations()
        {
            var config = new HashSet<dynamic>()
            {
                new RoomActionConfiguration(),
                new UserConfiguration(),
                new CommentConfiguration(),
                new HighFiveConfiguration()
            };

            return config;
        }
    }
}
