using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LocationIp
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<AsnBlock> AsnBlocks { get; set; }
        public virtual DbSet<CityBlock> CityBlocks { get; set; }
        public virtual DbSet<CityLocation> CityLocations { get; set; }
        public virtual DbSet<CountryBlock> CountryBlocks { get; set; }
        public virtual DbSet<CountryLocation> CountryLocations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<AsnBlock>(entity =>
            {
                entity.HasKey(e => e.AsnBlocksId)
                    .HasName("asn_blocks_pk");

                entity.ToTable("asn_blocks");

                entity.HasIndex(e => e.Network, "asn_blocks_network_idx")
                    .HasMethod("gist")
                    .HasOperators(new[] { "inet_ops" });

                entity.Property(e => e.AsnBlocksId)
                    .HasColumnName("asn_blocks_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AutonomousSystemNumber).HasColumnName("autonomous_system_number");

                entity.Property(e => e.AutonomousSystemOrganization)
                    .HasMaxLength(255)
                    .HasColumnName("autonomous_system_organization");

                entity.Property(e => e.Network).HasColumnName("network");
            });

            modelBuilder.Entity<CityBlock>(entity =>
            {
                entity.HasKey(e => e.CityBlocksId)
                    .HasName("city_blocks_pk");

                entity.ToTable("city_blocks");

                entity.HasIndex(e => e.Network, "city_blocks_network_idx")
                    .HasMethod("gist")
                    .HasOperators(new[] { "inet_ops" });

                entity.Property(e => e.CityBlocksId)
                    .HasColumnName("city_blocks_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AccuracyRadius).HasColumnName("accuracy_radius");

                entity.Property(e => e.GeonameId).HasColumnName("geoname_id");

                entity.Property(e => e.IsAnonymousProxy).HasColumnName("is_anonymous_proxy");

                entity.Property(e => e.IsSatelliteProvider).HasColumnName("is_satellite_provider");

                entity.Property(e => e.Latitude)
                    .HasPrecision(9, 6)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasPrecision(9, 6)
                    .HasColumnName("longitude");

                entity.Property(e => e.Network).HasColumnName("network");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(8)
                    .HasColumnName("postal_code");

                entity.Property(e => e.RegisteredCountryGeonameId).HasColumnName("registered_country_geoname_id");

                entity.Property(e => e.RepresentedCountryGeonameId).HasColumnName("represented_country_geoname_id");
            });

            modelBuilder.Entity<CityLocation>(entity =>
            {
                entity.HasKey(e => e.CityLocationsId)
                    .HasName("city_locations_pk");

                entity.ToTable("city_locations");

                entity.Property(e => e.CityLocationsId)
                    .HasColumnName("city_locations_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CityName)
                    .HasMaxLength(255)
                    .HasColumnName("city_name");

                entity.Property(e => e.ContinentCode)
                    .HasMaxLength(5)
                    .HasColumnName("continent_code");

                entity.Property(e => e.ContinentName)
                    .HasMaxLength(255)
                    .HasColumnName("continent_name");

                entity.Property(e => e.CountryIsoCode)
                    .HasMaxLength(5)
                    .HasColumnName("country_iso_code");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .HasColumnName("country_name");

                entity.Property(e => e.GeonameId).HasColumnName("geoname_id");

                entity.Property(e => e.IsInEuropeanUnion).HasColumnName("is_in_european_union");

                entity.Property(e => e.LocaleCode)
                    .HasMaxLength(5)
                    .HasColumnName("locale_code");

                entity.Property(e => e.MetroCode)
                    .HasMaxLength(5)
                    .HasColumnName("metro_code");

                entity.Property(e => e.Subdivision1IsoCode)
                    .HasMaxLength(5)
                    .HasColumnName("subdivision_1_iso_code");

                entity.Property(e => e.Subdivision1Name)
                    .HasMaxLength(255)
                    .HasColumnName("subdivision_1_name");

                entity.Property(e => e.Subdivision2IsoCode)
                    .HasMaxLength(5)
                    .HasColumnName("subdivision_2_iso_code");

                entity.Property(e => e.Subdivision2Name)
                    .HasMaxLength(255)
                    .HasColumnName("subdivision_2_name");

                entity.Property(e => e.TimeZone)
                    .HasMaxLength(255)
                    .HasColumnName("time_zone");
            });

            modelBuilder.Entity<CountryBlock>(entity =>
            {
                entity.HasKey(e => e.CountryBlocksId)
                    .HasName("country_blocks_pk");

                entity.ToTable("country_blocks");

                entity.HasIndex(e => e.Network, "country_blocks_network_idx")
                    .HasMethod("gist")
                    .HasOperators(new[] { "inet_ops" });

                entity.Property(e => e.CountryBlocksId)
                    .HasColumnName("country_blocks_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.GeonameId).HasColumnName("geoname_id");

                entity.Property(e => e.IsAnonymousProxy).HasColumnName("is_anonymous_proxy");

                entity.Property(e => e.IsSatelliteProvider).HasColumnName("is_satellite_provider");

                entity.Property(e => e.Network).HasColumnName("network");

                entity.Property(e => e.RegisteredCountryGeonameId).HasColumnName("registered_country_geoname_id");

                entity.Property(e => e.RepresentedCountryGeonameId).HasColumnName("represented_country_geoname_id");
            });

            modelBuilder.Entity<CountryLocation>(entity =>
            {
                entity.HasKey(e => e.CountryLocationsId)
                    .HasName("country_locations_pk");

                entity.ToTable("country_locations");

                entity.Property(e => e.CountryLocationsId)
                    .HasColumnName("country_locations_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ContinentCode)
                    .HasMaxLength(5)
                    .HasColumnName("continent_code");

                entity.Property(e => e.ContinentName)
                    .HasMaxLength(255)
                    .HasColumnName("continent_name");

                entity.Property(e => e.CountryIsoCode)
                    .HasMaxLength(5)
                    .HasColumnName("country_iso_code");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .HasColumnName("country_name");

                entity.Property(e => e.GeonameId).HasColumnName("geoname_id");

                entity.Property(e => e.IsInEuropeanUnion).HasColumnName("is_in_european_union");

                entity.Property(e => e.LocaleCode)
                    .HasMaxLength(5)
                    .HasColumnName("locale_code");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
