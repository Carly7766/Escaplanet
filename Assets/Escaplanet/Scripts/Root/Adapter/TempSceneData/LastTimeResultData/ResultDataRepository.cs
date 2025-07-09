using Escaplanet.Root.Domain.TempSceneData;
using Escaplanet.Root.Domain.TempSceneData.ResultData;

namespace Escaplanet.Root.Adapter.TempSceneData.LastTimeResultData
{
    public class ResultDataRepository : ITempDataRepository<ResultData, int>
    {
        private readonly ITempDataStore<ResultData> _resultDataStore;

        public ResultDataRepository(ITempDataStore<ResultData> resultDataStore)
        {
            _resultDataStore = resultDataStore;
        }

        public ResultData GetData()
        {
            return _resultDataStore.LoadTempData();
        }

        public void SetData(int data)
        {
            var resultData = new ResultData(LastTimeResultDataId.NewId(), data);
            _resultDataStore.RestoreTempData(resultData);
        }
    }
}