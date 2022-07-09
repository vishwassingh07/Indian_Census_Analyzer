using IndianStatesCensusAnalyser;
using IndianStatesCensusAnalyser.DTO;
using NUnit.Framework;
using System.Collections.Generic;

namespace IndianStatesCensusTesting
{
    public class Tests
    {
        string stateCensusFilePath = @"D:\RFP BATCH 157\Indian Census Analyzer\Indian_Census_Analyzer\IndianCensusAnalyzer\CSVFiles\IndianPopulation.csv";
        string wrongFilePath = @"D:\RFP BATCH 157\Indian Census Analyzer\Indian_Census_Analyzer\IndianCensusAnalyzer\CSVFiles\WrongIndianPopulation.csv";
        string wrongTypeFilePath = @"D:\RFP BATCH 157\Indian Census Analyzer\Indian_Census_Analyzer\IndianCensusAnalyzer\CSVFiles\IndiaStateCode.txt";
        string wrongDelimiterFilePath = @"D:\RFP BATCH 157\Indian Census Analyzer\Indian_Census_Analyzer\IndianCensusAnalyzer\CSVFiles\DelimiterIndiaStateCensusData.csv";
        string wrongHeaderFilePath = @"D:\RFP BATCH 157\Indian Census Analyzer\Indian_Census_Analyzer\IndianCensusAnalyzer\CSVFiles\WrongIndiaStateCensusData.csv";

        public CSVAdapterFactory csvAdapter = null;
        Dictionary<string, CensusDTO> stateRecords = null;

        [SetUp]
        public void SetUp()
        {
            csvAdapter = new CSVAdapterFactory();
            stateRecords = new Dictionary<string, CensusDTO>();
        }

        //TC 1.1 Given correct path To ensure number of records matches
        [Test]
        public void GivenStateCensusCsvReturnStateRecords()
        {
            int expected = 29;
            stateRecords = csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, stateCensusFilePath, "State,Population,AreaInSqKm,DensityPerSqKm");
            Assert.AreEqual(expected, stateRecords.Count);
        }

        //TC 1.2 Given incorrect file should return custom exception - File not found
        [Test]
        public void GivenWrongFileReturnCustomException()
        {
            var expected = CensusAnalyserException.ExceptionType.FILE_NOT_FOUND;
            var customException = Assert.Throws<CensusAnalyserException>(() => csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, wrongFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(expected, customException.exception);
        }

        //TC 1.3 Given correct path but incorrect file type should return custom exception - Invalid file type
        [Test]
        public void GivenWrongFileTypeReturnCustomException()
        {
            var expected = CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE;
            var customException = Assert.Throws<CensusAnalyserException>(() => csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, wrongTypeFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(expected, customException.exception);
        }

        //TC 1.4 Given correct path but wrong delimiter should return custom exception - File Containers Wrong Delimiter
        [Test]
        public void GivenWrongDelimiterReturnCustomException()
        {
            var expected = CensusAnalyserException.ExceptionType.INCOREECT_DELIMITER;
            var customException = Assert.Throws<CensusAnalyserException>(() => csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, wrongDelimiterFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(expected, customException.exception);
        }

        //TC 1.5 Given correct path but wrong header should return custom exception - Incorrect header in Data
        [Test]
        public void GivenWrongHeaderReturnCustomException()
        {
            var expected = CensusAnalyserException.ExceptionType.INCORRECT_HEADER;
            var customException = Assert.Throws<CensusAnalyserException>(() => csvAdapter.LoadCsvData(CensusAnalyser.Country.INDIA, wrongHeaderFilePath, "State,Population,AreaInSqKm,DensityPerSqKm"));
            Assert.AreEqual(expected, customException.exception);
        }

    }
}