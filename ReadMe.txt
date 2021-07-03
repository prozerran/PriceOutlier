

How to Run:
- Just compile the project and run it
- Make sure the startup project is set to PriceOutlier
- Additionally, you can just run the UnitTest to test the library/algorithm

[3 Projects]
PriceOutlier            - This is main UI executable with simple LiveChart.
                          No business logic here, just UI, not MVVM, not optimized
                          Or just run [PriceOutlier.exe]
                                1) Click Load Data Points button to load all points
                                2) Toggle checkbox to show/hide outlier points
PriceOutlierLib         - The business logic and project that contains the core outlier
                          reading, importing, detection, data structures, etc...
PriceOutlierUnitTest    - Simple unit test project
                          Only test business layer (library)


Notes
- For simplicity, did not use any CSV lib
- LiveChart is used
    - not very efficient charting system, rather slow
- No optimization is done on the UI or charting system
- Outlier detection algorithm is completely made-up, and not ideal as it probably can't handle all test cases
- Did not over complicate design as to leave room for changes

Assumptions
- CSV file does not continously grow with data in realtime
- First (few) data points of csv file is assumed correct with no error
- File only contains date, price data, no other data assumed
- Date is always assumed correct in cronological order
- Date is assumed unique and act as the key