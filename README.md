TechFrexConcertManager

The TechFrexConcertManager is a lightweight and user-friendly console-based application that allows users to manage concerts efficiently. It provides functionality to add, edit, list, and delete concerts, as well as save and load data to and from a file.

Features
	1.	Add Concerts: Enter details like location, performer, date, time, capacity, and price.
	2.	Edit Concerts: Update existing concert details.
	3.	View All Concerts: Display a list of all concerts.
	4.	Delete Concerts: Remove concerts by ID.
	5.	Save Concert Data: Save concert details to a file (Concerts.txt).
	6.	Load Concert Data: Load concert details from a file.
	7.	API Integration: Add concerts programmatically through an API.

How to Use
	1.	Clone the repository or download the source code.
	2.	Open the project in your preferred C# development environment (e.g., Visual Studio).
	3.	Build and run the project.
	4.	Use the console menu to:
	•	Add new concerts by providing required details.
	•	View the list of existing concerts.
	•	Edit or delete specific concerts by entering their unique IDs.
	•	Save or load concert data to/from Concerts.txt.

Data File

The project uses a simple text file, Concerts.txt, to store concert information. Each line contains:
ID|Location|Performer|DateAndTime|Capacity|Price.

Example

To add a concert:
	1.	Enter details like:
	•	Location: Stockholm Arena
	•	Performer: Coldplay
	•	Date and Time: 2024-12-15
	•	Capacity: 20,000
	•	Price: 1200 SEK
	2.	The system assigns a unique ID and confirms the concert has been added.

Prerequisites
	•	.NET Framework 6.0 or later
	•	Basic knowledge of working with console applications

Limitations
	•	No graphical interface; entirely console-based.
	•	Data is stored in plain text without encryption.

Future Improvements
	1.	Add a graphical user interface (GUI).
	2.	Implement database integration for better data handling.
	3.	Add support for exporting concert data to other formats (e.g., CSV or JSON).