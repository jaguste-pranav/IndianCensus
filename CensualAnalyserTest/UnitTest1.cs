using NUnit.Framework;
using System.Collections.Generic;
using IndianStateCensusAnalyzer;
using IndianStateCensusAnalyzer.DTO;
using static IndianStateCensusAnalyzer.CensusAnalyser;

namespace CensualAnalyserTest
{
    public class Tests
    {
        //CensusAnalyser.CensusAnalyser censusAnalyser;

        static string indianStateCensusHeaders = "State,Population,AreaInSqKm,DensityPerSqKm";
        static string indianStateCodeHeaders = "SrNo,State Name,TIN,StateCode";
        static string indianStateCensusFilePath = @"C:\Users\Pranav V Jaguste\source\repos\IndianStateCensusAnalyzer\CensualAnalyserTest\CsvFiles\IndiaStateCensusData.csv";
        static string wrongHeaderIndianCensusFilePath = @"C:\Users\Pranav V Jaguste\source\repos\IndianStateCensusAnalyzer\CensualAnalyserTest\CsvFiles\WrongIndiaStateCensusData.csv";
        static string delimiterIndianCensusFilePath = @"C:\Users\Pranav V Jaguste\source\repos\IndianStateCensusAnalyzer\CensualAnalyserTest\CsvFiles\DelimiterIndiaStateCensusData.csv";
        static string wrongIndianStateCensusFilePath = @"C:\Users\Pranav V Jaguste\source\repos\IndianStateCensusAnalyzer\CensualAnalyserTest\CsvFiles\WrongIndiaStateCensusData1.csv";
        static string wrongIndianStateCensusFileType = @"C:\Users\Pranav V Jaguste\source\repos\IndianStateCensusAnalyzer\CensualAnalyserTest\CsvFiles\IndiaStateCensusData.txt";
        static string indianStateCodeFilePath = @"C:\Users\Pranav V Jaguste\source\repos\IndianStateCensusAnalyzer\CensualAnalyserTest\CsvFiles\IndiaStateCode.csv";
        static string wrongIndianStateCodeFileType = @"C:\Users\Pranav V Jaguste\source\repos\IndianStateCensusAnalyzer\CensualAnalyserTest\CsvFiles\IndiaStateCode.txt";
        static string delimiterIndianStateCodeFilePath = @"C:\Users\Pranav V Jaguste\source\repos\IndianStateCensusAnalyzer\CensualAnalyserTest\CsvFiles\DelimiterIndiaStateCode.csv";
        static string wrongHeaderStateCodeFilePath = @"C:\Users\Pranav V Jaguste\source\repos\IndianStateCensusAnalyzer\CensualAnalyserTest\CsvFiles\WrongIndiaStateCode.csv";
        //US Census FilePath
        static string usCensusHeaders = "State Id,State,Population,Housing units,Total area,Water area,Land area,Population Density,Housing Density";
        static string usCensusFilepath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\USCensusData.csv";
        static string wrongUSCensusFilePath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\USData.csv";
        static string wrongUSCensusFileType = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\USCensusData.txt";
        static string wrongHeaderUSCensusFilePath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\WrongHeaderUSCensusData.csv";
        static string delimeterUSCensusFilePath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\DelimiterUSCensusData.csv";

        CensusAnalyser censusAnalyser;
        Dictionary<string, CensusDTO> totalRecord;
        Dictionary<string, CensusDTO> stateRecord;

        [SetUp]
        public void Setup()
        {
            censusAnalyser = new CensusAnalyser();
            totalRecord = new Dictionary<string, CensusDTO>();
            stateRecord = new Dictionary<string, CensusDTO>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void GivenIndianCensusDataFile_WhenReaded_ShouldReturnCensusDataCount()
        {
            totalRecord = censusAnalyser.LoadCensusData(indianStateCensusFilePath, Country.INDIA, indianStateCensusHeaders);
            stateRecord = censusAnalyser.LoadCensusData(indianStateCodeFilePath, Country.INDIA, indianStateCodeHeaders);
            Assert.AreEqual(29, totalRecord.Count);
            Assert.AreEqual(37, stateRecord.Count);
        }
        [Test]
        public void GivenWrongIndianCensusDataFile_WhenReaded_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCensusFilePath, Country.INDIA, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCensusFilePath, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, stateException.eType);
        }

        [Test]
        public void GivenWrongIndianCensusDataFileType_WhenReadReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCodeFileType, Country.INDIA, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCodeFileType, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, stateException.eType);
        }

        [Test]
        public void GivenIndianCensusDataFileType_ButIncorrectDelimeter_WhenReadReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(delimiterIndianCensusFilePath, Country.INDIA, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(delimiterIndianStateCodeFilePath, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, stateException.eType);
        }
         

       [Test]
        public void GivenIndianCensusDataFileType_ButIncorrectHeader_WhenReadReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(wrongHeaderIndianCensusFilePath, Country.INDIA, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(wrongHeaderStateCodeFilePath, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, stateException.eType);
        }
    }
}