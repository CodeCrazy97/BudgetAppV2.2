/*
 * CRUD for Google Sheets
 * Created: 08/05/2023.
 * By Ethan Vaughan
 */

using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System.Collections.Generic;
using System;

namespace BudgetApp_V2
{
    internal class GoogleSheetsService
    {
        private string apiKey;
        private string spreadsheetId;
        private SheetsService sheetsService;

        public GoogleSheetsService(string apiKey, string spreadsheetId)
        {
            this.apiKey = apiKey;
            this.spreadsheetId = spreadsheetId;
            // Initialize the SheetsService
            sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                ApiKey = apiKey
            });

            this.sheetsService = sheetsService;

        }


        public IList<IList<object>> ReadData(string spreadsheetId, string range)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange response = request.Execute();
            IList<IList<object>> values = response.Values;

            return values;
        }

        public IList<IList<object>> ReadAllRowsFromSheet()
        {
            List<IList<object>> allRows = new List<IList<object>>();

            string range = "A:D"; // Read all columns A to Z (adjust as needed)

            SpreadsheetsResource.ValuesResource.GetRequest request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
            ValueRange response = request.Execute();

            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                allRows.AddRange(values);
            }

            return allRows;
        }


        public bool DeleteRow(int i)
        {
            Request RequestBody = new Request()
            {
                DeleteDimension = new DeleteDimensionRequest()
                {
                    Range = new DimensionRange()
                    {
                        SheetId = 0,
                        Dimension = "ROWS",
                        StartIndex = Convert.ToInt32(i),
                        EndIndex = Convert.ToInt32(i)
                    }
                }
            };

            List<Request> RequestContainer = new List<Request>();
            RequestContainer.Add(RequestBody);

            BatchUpdateSpreadsheetRequest DeleteRequest = new BatchUpdateSpreadsheetRequest();
            DeleteRequest.Requests = RequestContainer;
            SpreadsheetsResource.BatchUpdateRequest Deletion = new SpreadsheetsResource.BatchUpdateRequest(sheetsService, DeleteRequest, spreadsheetId);
            var x = Deletion.Execute();
            return true;
        }
    }
}

