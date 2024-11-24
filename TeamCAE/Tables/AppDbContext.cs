using Microsoft.EntityFrameworkCore;

namespace TeamCAE.Tables {
    public class TeamCAEDbContext : DbContext {
        public TeamCAEDbContext(DbContextOptions<TeamCAEDbContext> options) : base(options) {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<FavoriteFood> FavoriteFoods { get; set; }
        public DbSet<FavoriteRide> FavoriteRides { get; set; }
    }
}