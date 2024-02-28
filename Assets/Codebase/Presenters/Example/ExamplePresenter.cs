using Assets.Codebase.Presenter.MainMenu;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Presenters.Example
{
    // Example presenter
    public class ExamplePresenter : BasePresenter, IExamplePresenter
    {
        private const string StartText = "Start";
        private const string StopText = "Stop";

        // Reactive property for each changable window element
        public ReactiveProperty<string> StartButtonText { get; private set; }
        public ReactiveProperty<string> SampleValueAmountString {  get; private set; } 

        public ExamplePresenter()
        {
            // Corresponding view id
            ViewId = ViewId.ExampleView;

            // Init all properties
            StartButtonText = new ReactiveProperty<string>(StartText);
            SampleValueAmountString = new ReactiveProperty<string>(0.ToString());
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
            // Subscribe to each model parameter of interest
            ProgressModel.SessionProgress.SampleValue.Subscribe(OnSampleValueChanged).AddTo(CompositeDisposable);
        }

        private void OnSampleValueChanged(int newGoldAmount)
        {
            SampleValueAmountString.Value = newGoldAmount.ToString();
        }

        public void OnStartButtonClicked()
        {
            if (StartButtonText.Value == StartText)
            {
                StartButtonText.Value = StopText;
            }
            else
            {
                StartButtonText.Value = StartText;
            }

            ProgressModel.SessionProgress.SampleValue.Value++;
        }
    }
}
