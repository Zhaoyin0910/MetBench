# MetBench-- A Numerical Expression Metamorphic Relations Benchmark Data Set

## 1. System Introduction:
The storage management system for numerical expression metamorphic relations is a comprehensive tool that integrates metamorphic relation management, automated execution and visualization.

## 2. Function Introduction:
There are three main functional modules, which are metamorphic relation management, automated execution and visualization, and intelligent recommendation of metamorphic relations. Users can query, add, delete, and modify metamorphic relation operations, as well as add, delete, and modify application programs and domain information, making it convenient for users to manage. Additionally, the system has the ability to automate the execution of metamorphic testing and visualize output data.

## 3. Software Environment Requirements:
- Microsoft Visual Studio 2022
- Python 3.8
- .Net 6.0
- MikTeX 23.10 / TeX Live 2023
- JPype1 1.4.1
- javalang 0.13.0
- Matplotlib 3.7.2
- SymPy 1.12
- antlr4-python3-runtime 4.11.0

## 4. Environment Configuration:
1. Install Microsoft Visual Studio:
   - The version used in this document is Community 2022.
   - Configure workloads by selecting ".NET desktop development."
   - Configure an individual component by selecting ".NET 6.0 Runtime (Long-Term Support)."

2. Install Python 3.8 and configure environment variables.

3. Install dependencies:
   - `pip install JPype1==1.4.1`
   - `pip install javalang==0.13.0`
   - `pip install matplotlib==3.7.2`
   - `pip install sympy==1.12`
   - `pip install antlr4-python3-runtime==4.11`

4. Configure Tex environment:
   - There are two choices for configuring the Tex environment, MiKTeX and TeXLive. It is recommended to install MiKTeX.
  
   - MikTeX Installation:
     - Visit the official website https://miktex.org/download to download and install.
     - Choose an installation path that does not contain Chinese characters or spaces.
     - Ensure the installation path directory is empty.

   - Run the installation file:
     - Choose the installation path, preferably without Chinese characters or spaces.
     - During the installation process, choose personal preferences, select A4 for Preferred, and Ask me first for Install missing packages.
     - Click "Start" to begin the installation.

   - Install packages:
     - Open the MikTex Console in cmd and click "Check for updates."
     - If package updates are needed, select all and click update now.
     - Install packages like type1cm, cm-super, geometry, underscore, zhmetrics, and other necessary packages.

## 5. Using the System:
After completing the environment configuration, import the MetBench project into Visual Studio to utilize the system functionalities.

