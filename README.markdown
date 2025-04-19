# MapperAI

- NuGet: MapperIA.Core

---

## Description

**MapperAI** is a project that harnesses artificial intelligence to simplify data mapping. It is designed to streamline the extraction of information from documents and enable users to transform data from various sources into organized and useful structures. Currently, MapperAI supports extracting information from PDF documents, mapping between different class types, and converting files from various programming languages to any other language, including C#.

**Note:** To use MapperAI, you need either a Gemini API key or a locally running AI model via Ollama.

## Current Features

MapperAI currently offers the following functionalities:

1. **PDF Text to Class Mapping**:

   - **Create a Class**: Define a class with attributes you want to extract from the PDF.
   - **Provide a PDF**: Upload a PDF file containing the text to be analyzed.
   - **AI Processing**: The AI analyzes the PDF text and matches values to the defined class attributes.
   - **Return Filled Class**: The project returns the class populated with data extracted from the PDF.

2. **Class Conversion**:

   - MapperAI enables seamless conversion between classes, such as mapping an `Entity` (e.g., `User`) to a `DTO` (e.g., `UserDTO`), making data transformation quick and efficient.

3. **Cross-Language File Conversion**:

   - Convert files from one programming language to any other language (e.g., `Student.py` to `Student.cs` or Java to Python). Users can also rename classes during the conversion process.

## Installation

To install **MapperAI**, use NuGet:

```bash
dotnet add package MapperIA.Core
```

## Project Status

**MapperAI** is under active development, with new features and improvements being planned to enhance user experience and mapping efficiency.

## Technologies Used

MapperAI is built with the following technologies:

- **C#**: The primary programming language for the project's logic.
- **JsonSerializer**: For serializing and deserializing data in JSON format, facilitating communication between the application and AI.
- **iText 7**: A powerful library for manipulating PDF files, used to extract text and information from documents.
- **Gemini AI**: Integrated AI for data mapping and extraction (requires an API key).
- **Ollama**: Optional local AI model support for running AI tasks without external API dependencies.

## AI Integration

MapperAI supports two AI options:

- **Cloud-Based AI**:

  - Requires a Gemini API key (sign up at Gemini API).

  - Set the API key in the configuration file or as an environment variable:

    ```bash
    export GEMINI_API_KEY="your-api-key"
    ```

  - Future integrations will include ChatGPT and other AI APIs.

- **Local AI**:

  - Run a local model using Ollama (ensure Ollama is installed and configured with a compatible model, e.g., LLaMA).
  - Update project settings to connect to your Ollama instance.

## Prerequisites

- **.NET SDK** (version 8.0 or higher).
- **AI Setup** (choose one):
  - Gemini API key for cloud-based AI.
  - Ollama installed for local AI processing.

## Contributing

Contributions are highly welcome! If you have ideas or suggestions to improve the project, feel free to open an issue or submit a pull request.

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/new-feature`).
3. Commit your changes (`git commit -m 'Add new feature'`).
4. Push to the remote repository (`git push origin feature/new-feature`).
5. Open a Pull Request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Contact

For questions, suggestions, or support, reach out via \[email or GitHub issues\].
