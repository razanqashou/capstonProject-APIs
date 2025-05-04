using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace capAPI.Models;

public partial class DBCapstoneContext : DbContext
{
    public DBCapstoneContext()
    {
    }

    public DBCapstoneContext(DbContextOptions<DBCapstoneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientAuthentication> ClientAuthentications { get; set; }

    public virtual DbSet<ClientLocation> ClientLocations { get; set; }

    public virtual DbSet<ClientWallet> ClientWallets { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverRate> DriverRates { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemOption> ItemOptions { get; set; }

    public virtual DbSet<ItemRate> ItemRates { get; set; }

    public virtual DbSet<LookupItem> LookupItems { get; set; }

    public virtual DbSet<LookupValue> LookupValues { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<OfferCategory> OfferCategories { get; set; }

    public virtual DbSet<OfferItem> OfferItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderRate> OrderRates { get; set; }

    public virtual DbSet<PaymentCard> PaymentCards { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionRole> PermissionRoles { get; set; }

    public virtual DbSet<PickupLocation> PickupLocations { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketAction> TicketActions { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripOrder> TripOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserOtpcode> UserOtpcodes { get; set; }

    public virtual DbSet<WalletTransaction> WalletTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS13;Database=DatabaseEdit;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B59D33B93");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ItemCount).HasDefaultValue(0);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A045AE7F974");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OrderCount).HasDefaultValue(0);

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Clients__ClientI__534D60F1");
        });

        modelBuilder.Entity<ClientAuthentication>(entity =>
        {
            entity.HasKey(e => e.AuthId).HasName("PK__ClientAu__12C15D3385535B35");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AuthProviderTypeNavigation).WithMany(p => p.ClientAuthentications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientAuthentication_LookupValue_AuthenticationProviderType");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientAuthentications).HasConstraintName("FK__ClientAut__Clien__571DF1D5");
        });

        modelBuilder.Entity<ClientLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__ClientLo__E7FEA477F21F43E6");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientLoc__Clien__55F4C372");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ClientLocationCreatedByNavigations).HasConstraintName("FK__ClientLoc__Creat__58D1301D");

            entity.HasOne(d => d.ProvinceLookup).WithMany(p => p.ClientLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientLoc__Provi__56E8E7AB");

            entity.HasOne(d => d.Region).WithMany(p => p.ClientLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientLoc__Regio__57DD0BE4");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ClientLocationUpdatedByNavigations).HasConstraintName("FK__ClientLoc__Updat__59C55456");
        });

        modelBuilder.Entity<ClientWallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PK__ClientWa__84D4F92E2969E82C");

            entity.Property(e => e.WalletId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.WalletBalance).HasDefaultValue(0.00m);

            entity.HasOne(d => d.Client).WithMany(p => p.ClientWallets).HasConstraintName("FK_ClientWallet_Clients");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Drivers__F1B1CD2466BBAA54");

            entity.Property(e => e.DriverId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Drivers).HasConstraintName("FK_Drivers_Users1");

            entity.HasOne(d => d.Employee).WithOne(p => p.Driver).HasConstraintName("FK_drivers_Users");

            entity.HasOne(d => d.VehicleTypeNavigation).WithMany(p => p.Drivers).HasConstraintName("FK_Driver_LookupValue_VehicleType");
        });

        modelBuilder.Entity<DriverRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DriverRa__3214EC276167F086");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Client).WithMany(p => p.DriverRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DriverRate_Clients");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DriverRate_Drivers");

            entity.HasOne(d => d.Order).WithMany(p => p.DriverRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverRat__Order__5D95E53A");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1EBF1548C");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.EmployeeTypeNavigation).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_LookupValue_EmployeeType");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Employees__Emplo__47DBAE45");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__CE74FAF50CF698C7");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Client).WithMany(p => p.Favorites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorites__Clien__681373AD");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FavoriteCreatedByNavigations).HasConstraintName("FK__Favorites__Creat__634EBE90");

            entity.HasOne(d => d.Item).WithMany(p => p.Favorites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorites__ItemI__662B2B3B");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.FavoriteUpdatedByNavigations).HasConstraintName("FK__Favorites__Updat__6442E2C9");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E83EBAC8D9C10");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDiscount).HasDefaultValue(false);
            entity.Property(e => e.ItemBadge).HasDefaultValue("New");

            entity.HasOne(d => d.Category).WithMany(p => p.Items).HasConstraintName("FK_Items_Categories");
        });

        modelBuilder.Entity<ItemOption>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__ItemOpti__92C7A1DF66734B31");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsRequired).HasDefaultValue(false);
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Item).WithMany(p => p.ItemOptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ItemOptio__ItemI__671F4F74");

            entity.HasOne(d => d.OptionCategory).WithMany(p => p.ItemOptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemOption_LookupValue_OptionCategoryID");
        });

        modelBuilder.Entity<ItemRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ItemRate__3214EC2796B58AB8");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Client).WithMany(p => p.ItemRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemRate_Clients");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemRate_Items");
        });

        modelBuilder.Entity<LookupItem>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LookupValue>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.LookupItem).WithMany(p => p.LookupValues)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LookupValue_LookupValue");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32715A0C11");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsRead).HasDefaultValue(false);

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_LookupValue_NotificationType");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.OfferId).HasName("PK__Offers__8EBCF0B16DCFA4B3");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OfferCreatedByNavigations).HasConstraintName("FK__Offers__CreatedB__76619304");

            entity.HasOne(d => d.OfferStatus).WithMany(p => p.Offers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Offers__OfferSta__756D6ECB");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OfferUpdatedByNavigations).HasConstraintName("FK__Offers__UpdatedB__7755B73D");
        });

        modelBuilder.Entity<OfferCategory>(entity =>
        {
            entity.HasKey(e => e.OfferCategoryId).HasName("PK__OfferCat__E1FD3C0609229E5C");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.OfferCategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OfferCate__Categ__70A8B9AE");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OfferCategoryCreatedByNavigations).HasConstraintName("FK__OfferCate__Creat__6DCC4D03");

            entity.HasOne(d => d.Offer).WithMany(p => p.OfferCategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OfferCate__Offer__6FB49575");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OfferCategoryUpdatedByNavigations).HasConstraintName("FK__OfferCate__Updat__6EC0713C");
        });

        modelBuilder.Entity<OfferItem>(entity =>
        {
            entity.HasKey(e => e.OfferItemId).HasName("PK__OfferIte__51A47B4D439DB80E");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OfferItemCreatedByNavigations).HasConstraintName("FK__OfferItem__Creat__719CDDE7");

            entity.HasOne(d => d.Item).WithMany(p => p.OfferItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OfferItem__ItemI__74794A92");

            entity.HasOne(d => d.Offer).WithMany(p => p.OfferItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OfferItem__Offer__73852659");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OfferItemUpdatedByNavigations).HasConstraintName("FK__OfferItem__Updat__72910220");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFE54C2189");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SecretCode).IsFixedLength();
            entity.Property(e => e.SecretCodeExpiry).HasDefaultValueSql("(dateadd(hour,(1),getdate()))");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders).HasConstraintName("FK__Orders__ClientID__5CD6CB2B");

            entity.HasOne(d => d.Driver).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Orders__DriverID__5DCAEF64");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.OrderPaymentMethods).HasConstraintName("FK_PaymentMethod");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.OrderPaymentStatuses).HasConstraintName("FK_Orders_LookupValue_PaymentStatus");

            entity.HasOne(d => d.PickupLocation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_PickupLocations");

            entity.HasOne(d => d.Status).WithMany(p => p.OrderStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_LookupValue_OrderStatus");
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__OrderHis__4D7B4ADD90A93B76");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OrderHistoryCreatedByNavigations).HasConstraintName("FK__OrderHist__Creat__7849DB76");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderHist__Order__7D0E9093");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OrderHistoryUpdatedByNavigations).HasConstraintName("FK__OrderHist__Updat__793DFFAF");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A1BFDA5F5B");

            entity.Property(e => e.DiscountValue).HasDefaultValue(0m);

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__ItemI__76969D2E");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__Order__75A278F5");
        });

        modelBuilder.Entity<OrderRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderRat__3214EC27EC2ADCA2");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Client).WithMany(p => p.OrderRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderRate_Clients");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderRate__Order__01D345B0");
        });

        modelBuilder.Entity<PaymentCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__PaymentC__55FECD8EC72E144B");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.Last4Digits).IsFixedLength();

            entity.HasOne(d => d.Client).WithMany(p => p.PaymentCards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentCards_Clients");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PaymentCards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentCa__Payme__59C55456");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB0FC36F769D");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreatedBy).HasDefaultValue("SuperAdmin");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedBy).HasDefaultValue("SuperAdmin");
        });

        modelBuilder.Entity<PermissionRole>(entity =>
        {
            entity.HasKey(e => e.PermissionRoleId).HasName("PK__Permissi__DCB76D6CDA3BD955");

            entity.HasOne(d => d.Permission).WithMany(p => p.PermissionRoles).HasConstraintName("FK__Permissio__Permi__36B12243");

            entity.HasOne(d => d.Role).WithMany(p => p.PermissionRoles).HasConstraintName("FK__Permissio__RoleI__35BCFE0A");
        });

        modelBuilder.Entity<PickupLocation>(entity =>
        {
            entity.HasKey(e => e.PickupLocationId).HasName("PK__PickupLo__F6FC9D08B0B23A46");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasDefaultValue("Main restaurant pickup location");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasDefaultValue(31.955200000000001);
            entity.Property(e => e.Longitude).HasDefaultValue(35.945);
            entity.Property(e => e.Name).HasDefaultValue("Main Branch");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PickupLocationCreatedByNavigations).HasConstraintName("FK__PickupLoc__Creat__0880433F");

            entity.HasOne(d => d.ProvinceLookup).WithMany(p => p.PickupLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PickupLoc__Provi__0D44F85C");

            entity.HasOne(d => d.Region).WithMany(p => p.PickupLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PickupLoc__Regio__0E391C95");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PickupLocationUpdatedByNavigations).HasConstraintName("FK__PickupLoc__Updat__09746778");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PK__Regions__ACD844431FEB82F2");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RegionCreatedByNavigations).HasConstraintName("FK__Regions__Created__0B5CAFEA");

            entity.HasOne(d => d.ProvinceLookup).WithMany(p => p.Regions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Regions__Provinc__11158940");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.RegionUpdatedByNavigations).HasConstraintName("FK__Regions__Updated__0C50D423");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A710485EC");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CreatedBy).HasDefaultValue("SuperAdmin");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedBy).HasDefaultValue("SuperAdmin");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Ticket__712CC627E3A41A76");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Client).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__ClientID__16CE6296");

            entity.HasOne(d => d.StatusIssue).WithMany(p => p.TicketStatusIssues).HasConstraintName("FK__Ticket__StatusIs__14E61A24");

            entity.HasOne(d => d.StatusSuggestion).WithMany(p => p.TicketStatusSuggestions).HasConstraintName("FK__Ticket__StatusSu__15DA3E5D");

            entity.HasOne(d => d.TicketType).WithMany(p => p.TicketTicketTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ticket__TicketTy__13F1F5EB");
        });

        modelBuilder.Entity<TicketAction>(entity =>
        {
            entity.HasKey(e => e.ActionId).HasName("PK__TicketAc__FFE3F4B98F22AF29");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ActionType).WithMany(p => p.TicketActions).HasConstraintName("FK__TicketAct__Actio__18B6AB08");

            entity.HasOne(d => d.Admin).WithMany(p => p.TicketActionAdmins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TicketAct__Admin__13F1F5EB");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TicketActionCreatedByNavigations).HasConstraintName("FK__TicketAct__Creat__11158940");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketActions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TicketAct__Ticke__17C286CF");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.TicketActionUpdatedByNavigations).HasConstraintName("FK__TicketAct__Updat__1209AD79");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4B0839F3C9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TransactionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Order).WithMany(p => p.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Order__1C873BEC");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Payme__1D7B6025");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("PK__Trips__51DC711E5D82069A");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StartTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TripStatusId).HasDefaultValue(1);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Trips)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trips__CreatedBy__1C873BEC");

            entity.HasOne(d => d.Driver).WithMany(p => p.Trips)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trips__DriverID__22401542");

            entity.HasOne(d => d.TripStatus).WithMany(p => p.Trips)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trip_LookupValue_TripStatus");
        });

        modelBuilder.Entity<TripOrder>(entity =>
        {
            entity.HasKey(e => e.TripOrderId).HasName("PK__TripOrde__49E0A9782727574A");

            entity.Property(e => e.AssignedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TripOrderStatusId).HasDefaultValue(1);

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.TripOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TripOrder__Assig__19AACF41");

            entity.HasOne(d => d.Order).WithMany(p => p.TripOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TripOrder__Order__1F63A897");

            entity.HasOne(d => d.Trip).WithMany(p => p.TripOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TripOrder__TripI__1E6F845E");

            entity.HasOne(d => d.TripOrderStatus).WithMany(p => p.TripOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trip_LookupValue_OrderStatus");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC5CDBF1C9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsLoggedIn).HasDefaultValue(false);
            entity.Property(e => e.IsVerified).HasDefaultValue(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasConstraintName("FK__Users__RoleID__4316F928");
        });

        modelBuilder.Entity<UserOtpcode>(entity =>
        {
            entity.HasKey(e => e.Otpid).HasName("PK__UserOTPC__5C2EC562CD59DFAF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.UserOtpcodes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserOTPCo__UserI__1E6F845E");
        });

        modelBuilder.Entity<WalletTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__WalletTr__55433A4B4D88191F");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TransactionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Client).WithMany(p => p.WalletTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WalletTransactions_Clients");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.WalletTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WalletTra__Payme__2704CA5F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
