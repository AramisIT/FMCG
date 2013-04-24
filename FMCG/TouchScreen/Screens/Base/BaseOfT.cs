using TouchScreen.Models.Data;

namespace AtosFMCG.TouchScreen.Screens.Base
{
    //Виніс в окремий файл щоб не заважало під рукою
    //WinForms не дозволяє оперувати візуальним конструктором якщо клас визначено як Class<T>
    public class BasePlannedArrival: BaseScreen<PlannedArrivalData>
    {
        public BasePlannedArrival(){}
        public BasePlannedArrival(PlannedArrivalData data) : base(data) { }
        protected override void initScreen() { }
    }
}