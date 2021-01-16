<h1 align="center">~Dr.Sillystringz's Factory~</h1>
<div align="center">
<img src="https://github.com/MaxBrockbank.png" width="200px" height="auto" >
</div>
<p align="center">Authored by Max Brockabnk</p>
<p align="center">Updated on Friday, January 8th, 2021</p>

# Description
This project is a practice in using C# ASP.NET Core along with Entity Framework Core, Identity, MySQL and MySQL Workbench to create a web app for a our good friend Pierre's Bakery to market the differnt flavors of treats and the different treats that he serves.

## Required Technologies
* C# .NET Core Ver 2.2.0
* MySQL Ver 8.0.15 and MySQL Workbench
* Text editor
* Modern web browser 

### Set-up Instructions

#### Cloning this repo
1. Begin by clicking on this green button on the right side of the screen that says 'Code'.
2. To clone the repo you'll want to click on the clipboard icon next to where you see a URL in a text input. This will copy this url for the repo to your clipboard
3. Open your computer termianl and navigate to whichever directory you'd like this project to live in.
4. Then run the command `git clone (Paste URL here)`. Additionally if you'd like to give this project's root directory a specific name you can run `git clone (Paste URL here) (New directory name)`. (Do not include the parens in these commands)
5. Run `cd SillyStringFactory.Solution` or `cd (Whatever you changed the directory name to)` to then navigate to the project root directory.

#### Generate MySQL Database Using Entity Framework Core
1. In your terminal navigate to the SweetNSavory.Solution/SweetNSavory directory. `cd SweetNSavory`.
2. Run the command `dotnet ef database update`. Entity Framework Core will then generate the database structure using the included migration files. 
3. __OPTIONALLY__, should you need to make changes to the code that adjust the structure of the databse, you can apply those updates with the following two commands. 
* `dotnet ef mirgrations add (Migration name here)`
* `dotnet ef database update`


### Setting up `appsettings.json`
1. Create file named `appsettings.json` 
2. Put the following code in the file
```
{
  "ConnectionStrings":{
    "DefaultConnection": "Server=localhost;Port=PORT_NUMBER;database=sweetnsavory;uid=USER;pwd=PASSWORD"
  }
}
```
3. Replace `PORT_NUMBER`, `USER`, and `PASSWORD` with your machines configurations.


## Technologies Used
* C# .NET Core
* ASP.NET Core
* ASP.NET Core Razor Pages
* Entity Framework Core
* MySQL

## Known Bugs
* No currently known bugs, please contact me if any found 


## Contact 
* [maxbrockbank1999@gmail.com](mailto:maxbrockbank1999@gmail.com)


## Legal
* Copyright © 2020 Max Brockbank
* This software is licensed under the MIT license