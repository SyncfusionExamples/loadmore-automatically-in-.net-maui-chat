# loadmore-automatically-in-.net-maui-chat
This repository contains a sample demonstrating how to load previous messages automatically in .NET MAUI Chat (SfChat).

## Sample

```xaml  

    <ContentPage.BindingContext>
        <local:ViewModel/>
    </ContentPage.BindingContext>

    <ContentPage.Content>
        <sfchat:SfChat x:Name="sfChat" 
                     Messages="{Binding Messages}" 
                     CurrentUser="{Binding CurrentUser}"
                     IsLazyLoading="{Binding IsBusy}"
                     LoadMoreCommand="{Binding LoadMoreCommand}" 
                     LoadMoreBehavior="{Binding LoadMoreBehavior}" >
        </sfchat:SfChat>
    </ContentPage.Content>

ViewModel:

    public partial class ViewModel : INotifyPropertyChanged
    {      
        private LoadMoreOption loadMoreBehavior = LoadMoreOption.Auto;

        public ICommand LoadMoreCommand { get; set; }

        public bool LoadMoreBehavior
        {
            get { return this.loadMoreBehavior; }
            set
            {
                this.loadMoreBehavior = value;
                RaisePropertyChanged("LoadMoreBehavior");
            }
        }

        public ObservableCollection<object> Messages
        {
            get{ return this.messages; }
            set
            {
                this.messages = value;
                RaisePropertyChanged("Messages");
            }
        }

        public LoadMoreViewModel()
        {
            this.Messages = CreateMessages();
            LoadMoreCommand = new Command<object>(LoadMoreItems, CanLoadMoreItems);
        }
    
        private bool CanLoadMoreItems(object obj)
        {
            // If messages are still there in the old message collection, then execute the load more command.
            if (this.OldMessages.Count > 0)
            {
                return true;
            }
            else
            {
                // Set the load more behavior of chat to none from auto to cancel the load more operation.
                this.LoadMoreBehavior = LoadMoreOption.None;
                return false;
            }

            return true;
        }
    
        private async void LoadMoreItems(object obj)
        {
            try
            {
                // Set is busy as true to show the busy indicator.
                IsBusy = true;
                await Task.Delay(3000);
                LoadMoreMessages();
            }
            catch{ }
            finally
            {
                // Set is busy as false to hide the busy indicator.
                IsBusy = false;
            }
        }

        private void LoadMoreMessages()
        {        
            for (int i = 1; i <= 10 ; i++)
            {
                var oldMessage = this.OldMessages[this.OldMessages.Count - 1];
                this.Messages.Insert(0, oldMessage);
                this.OldMessages.Remove(oldMessage);
            }
        }
    }
```

## Requirements to run the demo

To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)

## Troubleshooting:
### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion® has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion® liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion®'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion®'s samples.