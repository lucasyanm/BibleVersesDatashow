# "Bíblia em Slide" (Bible Verse Datashow)

## Overview

**Bible Verse Datashow** is a Windows-only .NET MAUI desktop application designed to display Bible verses in a presentation style on a separate window. The application allows users to search for Bible verses by book name, chapter, and verse, and display them on a presentation window. The user can also control the font size of the verses displayed in the presentation window, making it ideal for church services, Bible study sessions, or any scenario where Bible verses need to be presented visually to an audience. Currently, the only available language is PT-BR.

## Features

- **Search and Display Bible Verses:**

  - Users can search for Bible verses by typing the book name, chapter, and verse.
  - The selected verse is displayed on a separate presentation window.

- **Font Size Adjustment:**

  - The main window provides an option to adjust the font size of the verses displayed on the presentation window. This is also possible focusing on the presentation window.

- **Navigation:**
  - In the presentation window, users can navigate between verses using the left and right arrow keys.
  - Font size can be adjusted directly from the presentation window using the up and down arrow keys.

## Project Structure

- **Main Window:**

  - The main interface where users can search for Bible verses, select the chapter and verse, and adjust the font size for the presentation window.

- **Presentation Window:**
  - A separate window designed for displaying the selected Bible verses.
  - Users can navigate through the verses and adjust the font size directly from this window.

## Getting Started

### Prerequisites

- .NET 7.0 SDK or later
- Visual Studio 2022 or later with .NET MAUI workload installed

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/Bible Verse Datashow.git
   ```
2. Open the project in Visual Studio:

   ```bash
   cd Bible Verse Datashow
   start Bible Verse Datashow.sln
   ```

3. Build the solution:

   - Ensure that the build configuration is set to **Debug** or **Release** and **Windows Machine** is selected as the target.

4. Run the project:
   - Press `F5` to start the application.

### Usage

1. **Search for a Verse:**

   - In the main window, select the desired Bible book from the dropdown.
   - Enter the chapter and verse number.
   - The verse will be displayed on the presentation window in.

2. **Adjust Font Size:**

   - Use the font size option in the main window to set the preferred font size.
   - Alternatively, use the up and down arrow keys in the presentation window to increase or decrease the font size.

3. **Navigate Verses:**
   - Use the left and right arrow keys in the presentation window to navigate back and forth between verses.

### Customization

- **Bible Data:**

  - The Bible text is sourced from a JSON file located in the `./Resources/Bible/` directory.
  - You can replace or modify this file to change the Bible translation or add additional content.

- **UI Customization:**
  - The application's user interface can be customized by editing the XAML files in the project.

### Contribution

If you'd like to contribute to this project, please fork the repository and submit a pull request with your changes. All contributions are welcome!

### License

This project is licensed under the MIT License. See the `LICENSE` file for details.

### Contact

For further information or questions, please reach out to the project maintainer through [LinkedIn](https://www.linkedin.com/in/lucas-yan-carvalho/).
