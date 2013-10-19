using TouchScreen.Models.Data;

namespace AtosFMCG.TouchScreen.Screens.Base
{
    //Виніс в окремий файл щоб не заважало під рукою
    //WinForms не дозволяє оперувати візуальним конструктором якщо клас визначено як Class<T>
    public class BaseAcceptancePlan: BaseScreen<AcceptancePlanData>
    {
        public BaseAcceptancePlan(){}
        public BaseAcceptancePlan(AcceptancePlanData data) : base(data) { }
        protected override void initScreen() { }
    }
}