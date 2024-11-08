# Time of your Life

## Description

Time of Your Life is a project that will always be in "progress".  It's got a little bit of a start, but is intentionally left with need of feature implementations to get it to an [MVP] state.

The Time of Your Life project is designed to be a starting point for developers to show what kind of skills they've picked up over time.  There are many different enhancements suggested for this project.  They allow a developer to give a good idea of their ability to handle data design, backend components or client-side interface development tasks.

While it is important to see what kind of quality features and improvements a developer can add,  it is just as important to show a consistent commit history with clear concise commit comments in either a fork or branch of this repository. Seeing how a developer works over time provides a lot of insight into their experience and ability to work through coding problems.

This project was bootstrapped with [Create React App].  It uses a [.Net 8] Web Application to host a [React JS] framework client.

## How to Develop

Time of Your Life is built on these technologies:

- [.NET 8]
- [React JS]

To develop on this project clone this project, and open with an IDE of your choice. VS Code or Visual Studio 2019 or newer is recommended.

The project can be run from the command line using the command `dotnet run`.  The service and application hosting can be stopped by pressing `Ctrl+C` on Windows and Linux, or `Cmd+C` on macOS.

Changes to the React Client Side SPA, will be reflected in the UI in a hot load manner. Changes to the .Net server components will require a recompile, `dotnet build` and restart, `dotnet run`.

## TODOs

### Easier

#### Client

- [X] Make the title of the clock customizable

- [X] Make a return character in the text settings fields save the field property value

- [X] Add validation to the text input fields. Diego notes: both clock title and font do validate empty strings

- [X] Display the list of the presets saved on the server served from this endpoint:

  `GET: https://localhost:7154/clock/presets`

- [X] Add sync to the server time.

#### Server

- [X] Add preset save validation. Diego notes: DB classes have data annotations, yo you can't send empty values on validated fields
- [X] Add a unique identifier for a preset either an auto number value or a GUID. Diego notes: an auto number is generated from the SQLite insert
- [X] Add sync to the server time

### Medium-er

#### Client

- [X] Make the clock `Set Clock Props` panel expandable and collapsible. Diego notes: panel appears/dissappears with the +/- icon
- [X] Make the font size selection a slider control. Diego notes: Both title and clock strings are managed by independent sliders.
- [X] Make the colors of the title and the clock different color values. Diego notes: color pickers where added for clock and title. RGB values are now saved
- [X] Save the presets to the server at this endpoint:
  `POST: https://localhost:7154/clock/presets`
- [X] Select a preset from the list saved on the server to replace the current values and update the clock. Diego notes: When you check a saved preset from the list, it will update all values with selected values.
- [ ] Add unit tests

#### Server

- [X] Return a preset by the title ID. Diego notes: try https://localhost:7154/clock/presets/ID_NUMBER
- [X] Add the ability to update existing presets with changes in values. Diego notes: you can update a preset if you want to save and a prset is checked, this calls endpoint `PUT: https://localhost:7154/clock/presets`. If you want it to save as a new one instead, no preset should be checked from the list.
- [X] Add unit tests. Diego notes: didn´t have enough time to complete all tests :/
- [X] Add `ClockController` logging. Diego notes: Logging is added to all calls when you start the process, finishes and, even, if there's a error(i.e. model not valid, not found, etc.)

### Harder

#### Client

- [X] Add a color picker to select the font colors for multiple colors. Diego notes: Title and clock now have a color picker. It changes according your selection
- [X] Add the ability to print the time out in text, i.e. "1:32 PM" would be printed out as "one thirty two PM". Diego notes: there's a checkbox right above the title editor. If you check it, time will change to text and constantly updating because server sync. If you uncheck it, it will go back to default.
- [ ] Add an alarms control/presets
- [ ] Add a testing results output report
- [X] Add time zone to the preset and/or be able display multiple clocks/presets at the same time. Diego notes: A combobox with all timezones is added. if you click on "add", a new clock will appear in the selected time zone.

#### Server

- [X] Implement a [sqlite] database storage layer. Diego notes: A sqlite database called time_of_your_life will be created if it's not found on the main root. The solution has a database layer to handle all controller actions
- [X] Save alarms and populate the alarms on the clock. Diego notes: Due to time, only create and update were implemented. However, there's no integration with frontend :/
Enpoints are `POST: https://localhost:7154/clock/alarms` and `PUT: https://localhost:7154/clock/alarms`
- [ ] Add a testing results output report
- [X] Add time zone to the preset and/or be able display multiple clocks/presets at the same time. Diego notes: all time zones added on the frontend will go to the datebase through endpoint `POST https://localhost:7154/clock/timezones`. All time zones will be listed with endpoint `GET https://localhost:7154/clock/timezones`

#### Final Diego Note
Every time the project loads, it will check if there are saved presets. If none is found, an alert will appear saying "There are no presets saved. Default values will load". As long as there's a saved preset, this alert will not appear.

<!-- links -->

[Create React App]: https://github.com/facebookincubator/create-react-app "Create React App"
[MVP]: https://www.agilealliance.org/glossary/mvp/ "AGILE GLOSSARY: Minimum Viable Product (MVP)"
[sqlite]: https://www.sqlite.org/index.html "What Is SQLite"
[.NET 8]: https://dotnet.microsoft.com/en-us/download "Download .NET"
[React JS]: https://react.dev/ "React: The library for web and native user interfaces"
