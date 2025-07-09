using Escaplanet.Root.Adapter.TempSceneData;
using Escaplanet.Root.Domain.TempSceneData.ResultData;

namespace Escaplanet.Root.Framework.TempSceneData.ResultData
{
    public class ResultDataStore : ITempDataStore<Escaplanet.Root.Domain.TempSceneData.ResultData.ResultData>
    {
        private Escaplanet.Root.Domain.TempSceneData.ResultData.ResultData tempData;

        public ResultDataStore()
        {
            tempData = new Escaplanet.Root.Domain.TempSceneData.ResultData.ResultData(
                LastTimeResultDataId.NewId(), -1);
        }

        public void RestoreTempData(Escaplanet.Root.Domain.TempSceneData.ResultData.ResultData data)
        {
            tempData = data;
        }

        public Escaplanet.Root.Domain.TempSceneData.ResultData.ResultData LoadTempData()
        {
            return tempData;
        }
    }
}