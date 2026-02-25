# OpenBreviary - A Prayer Companion for Catholics

## 1. Overview
OpenBreviary is a prayer app for Catholics for daily Lauds, Vespers, and Compline based on the lectionary. 

## 2. Architecture
### Planning:
- I/O Protocols - How are the psalters, readings, notifications, etc delivered?
notifications could be through set timers and the readings and psalters being stored in a database (SQLite?)
- Serialization (Parsing) - How do we parse the data for the day? How do we display the data?
- Resilience & Storage - Do we need to remember anything or store anything? Do we need caching? If we keep everything local how heavy will that be?
- Observability - Do we need logging and metrics? For what parts of the app?
 
## Licensing
This project is an open-source liturgical resource. To ensure the software remains accessible and the content remains "in the commons," we use a dual-licensing model:

- Software & Logic: The code (HTML/CSS/JS/Python/etc.) is licensed under the MIT License. You are free to use, copy, and modify the code for any purpose.

- Breviary Content: All liturgical texts, Bible translations (using the WEB-CE bible translation), and the 4-week Psalter arrangement are licensed under [(CC BY-SA 4.0)](https://creativecommons.org/licenses/by-sa/4.0/).
 
## TODO
- [ ] Implement Psalter Week Calculator
- [ ] Get Start of Advent of previous year to have a proper understanding of the season
- [ ] Season calculator logic to be reviewed for ranking


[![License: CC BY-SA 4.0](https://img.shields.io/badge/License-CC%20BY--SA%204.0-lightgrey.svg)](https://creativecommons.org/licenses/by-sa/4.0/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
