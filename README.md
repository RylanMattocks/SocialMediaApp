# Drop-Tables
The innovative new social media account by group 2 at revature

The minimum viable product is:
-  a site with a home page listing post from followed users. 
-  The user can make their own posts consisting of text and maybe an image. 
-  There wil be a profile page displaying user posts with a profile description and profile picture.
-  Profile should also have a follow
-  Has a login and registration.

Stretch goals:
(potentially autho)
-  Ability to search users.
-  Homepage displays random posts when none to display. (Recomended feed)
-  Gameification (Challenges, badges, popularity)
-  Sub posts.

![Team document (3)](https://github.com/user-attachments/assets/f7e3b74c-df0c-4e35-a623-ef31a3a6e704)


## Server Admin Login

- **Username:** `dropTablesAdmin`
- **Password:** `ByeTables2024!`

## Database Information

- **Database Name:** `DropTablesDb`

## Connection String

```text
Server=tcp:droptables.database.windows.net,1433;Initial Catalog=DropTablesDb;Persist Security Info=False;User ID=dropTablesAdmin;Password=ByeTables2024!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```


## Setting Up User Secrets in .NET

### Enable Secret Storage

To use user secrets in your .NET project, you need to initialize secret storage. This can be done using the command line:

1. Open your terminal or command prompt.
2. Navigate to your project directory.
3. Run the following command:

   ```bash
   dotnet user-secrets init
   ```
   ```bash
   dotnet user-secrets set "ConnectionStrings:DropTablesDb" "Server=tcp:droptables.database.windows.net,1433;Initial Catalog=DropTablesDb;Persist Security Info=False;User ID=dropTablesAdmin;Password=ByeTables2024!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
   ```

