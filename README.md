# Video Rental Store
### Student project

The purpose of this project is to manage the distribution and return of films. You can easily customize this project for rental cars, bicycles and so on. Some feature: adding a client card, adding a film with past parts, validating for input fields, organizing the distribution and return of a film, viewing a client and film card, searching for clients and films, saving a client card to an Exel file, the history of all returned client films, display on the main screen the number of clients and films at the moment in the database. And there are also options when the films are over at the rental store, the number of films on hand is exceeded (5 films), the films chosen by the client did not repeat, the age check (18+ movie).

This project is created on Windows Forms with .NET Framework 4.6.1 and Microsoft SQL Server Compact 4.0.. 

#### Mini guide for project startup:
To connect the database and work with the tables was used [SQLite Toolbox](https://marketplace.visualstudio.com/items?itemName=ErikEJ.SQLServerCompactSQLiteToolbox). You can download it in Visual Studio Marketplace from Tools>Extensions and Updates>Online and in Search write "sqlite". Also you need to connect the database to the project in SQLite Toolbox. Open toolbox and press button "Add SQL CE 4.0 Connection" then press "Browse" and go to the path "...\WindowsFormsApp1\WindowsFormsApp1\WindowsFormsApp1\bin\Debug" and choose VRSDB.sdf. Now press ctrl+f5 for compilation. And you see this window:

![MainWindow](https://github.com/OneWhiteSpirit/VideoRentalStore/blob/master/Screenshots/MainWindow.png)
