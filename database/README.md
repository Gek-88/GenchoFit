# GenchoFit database

## Option A: XAMPP + phpMyAdmin

1. Start MySQL in XAMPP.
2. Open phpMyAdmin and choose **Import**.
3. Select `database/init.sql` and run it.
4. Keep the default connection in `appsettings.json` if the XAMPP root password is empty.

If MySQL has a password, update `ConnectionStrings:DefaultConnection` in `appsettings.json` before starting the app.

## Option B: Docker

From the project root run:

```bash
docker compose up -d
```

The compose file creates MySQL on port `3306`, database `genchofit`, user `root` and password `genchofit-local`. For this option set the connection string to:

```text
server=localhost;port=3306;database=genchofit;user=root;password=genchofit-local
```

The SQL file is mounted into the MySQL initialization directory and is run automatically on the first creation of the volume. `EnsureCreated()` in the app is also kept as a safety net.

There are no EF migrations required for this demo. `init.sql` is the portable database setup file.
