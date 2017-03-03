using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;

namespace Price.Services
{
    public class BaseAzureRest<TObject>
    {
        protected readonly IMobileServiceClient Client;
        protected readonly IMobileServiceSyncTable<TObject> Table;
        const string DbPath = "PriceApp";
        protected const string ServiceUri = "http://price-app.azurewebsites.net/";

        public BaseAzureRest()
        {
            Client = new MobileServiceClient(ServiceUri);
            var store = new MobileServiceSQLiteStore(DbPath);
            store.DefineTable<TObject>();
            Client.SyncContext.InitializeAsync(store);
            Table = Client.GetSyncTable<TObject>();
        }

        public async Task<IEnumerable<TObject>> GetAll()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected) await SyncAsync();

                return await Table.ToEnumerableAsync();
            }
            catch (Exception)
            {
                return new TObject[0];
            }
        }

        public async Task AddAsync(TObject entity)
        {
            await Table.InsertAsync(entity);
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await Client.SyncContext.PushAsync();

                await Table.PullAsync($"All{nameof(TObject)}", Table.CreateQuery());
            }
            catch (MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null) syncErrors = pushEx.PushResult.Errors;
            }
        }

        public async Task CleanData()
        {
            await Table.PurgeAsync($"All{nameof(TObject)}", Table.CreateQuery(), new CancellationToken());
        }
    }
}