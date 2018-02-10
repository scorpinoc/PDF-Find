using View.Cultures;

namespace View
{
    public class CultureSwitcher : ViewModel.ResourceSwitcher
    {
        public Resources Resources { get; private set; }

        protected override void LoadResource()
        {
            Resources.Culture = CurrentCulture;
            Resources = new Resources();
        }
    }
}
