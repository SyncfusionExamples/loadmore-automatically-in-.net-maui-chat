using Syncfusion.Maui.Chat;
using Syncfusion.Maui.ListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatMaui
{
    internal class ViewModel:INotifyPropertyChanged
    {
        #region Fields

        internal ObservableCollection<Author>? AuthorsCollection
        {
            get;
            set;
        }

        private int[] authorArray;

        private List<string>? messageList;

        private ObservableCollection<object> messages;

        private Author? currentUser;

        private ObservableCollection<object> oldMessages;

        private bool isBusy = false;

        private LoadMoreOption loadMoreBehavior;

        #endregion

        #region Public Properties

        /// <summary>
        /// Command to load more items.
        /// </summary>
        public ICommand LoadMoreCommand { get; set; }

        /// <summary>
        /// Gets or sets the behavior for loading more items.
        /// </summary>
        public LoadMoreOption LoadMoreBehavior
        {
            get
            {
                return this.loadMoreBehavior;
            }
            set
            {
                this.loadMoreBehavior = value;
                this.RaisePropertyChanged("LoadMoreBehavior");
            }
        }

        /// <summary>
        /// Collection of messages.
        /// </summary>
        public ObservableCollection<object> Messages
        {
            get
            {
                return this.messages;
            }

            set
            {
                this.messages = value;
            }
        }

        /// <summary>
        /// Collection of old messages.
        /// </summary>
        public ObservableCollection<object> OldMessages
        {
            get
            {
                return this.oldMessages;
            }

            set
            {
                this.oldMessages = value;
            }
        }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        public Author? CurrentUser
        {
            get
            {
                return this!.currentUser;
            }
            set
            {
                this!.currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view model is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ViewModel class.
        /// </summary>
        public ViewModel()
        {
            this.messages = new ObservableCollection<object>();
            this.oldMessages = new ObservableCollection<object>();
            this.InitializeAuthors();
            this.InitializeMessageList();
            this.CurrentUser = this.AuthorsCollection![0];
            this.authorArray = new int[] { 0, 1, 2, 1, 0, 1, 0, 2, 1, 0, 0, 2, 0, 1, 0, 2, 1, 0, 2, 1, 0, 2, 1, 0, 2, 1, 0, 2, 0, 1, 0, 2, 0, 2, 1, 0, 2, 0, 2, 1, 2, 0, 0, 2, 0, 1, 0, 2, 0, 1, 0, 1, 0, 2, 0, 0, 1, 2, 0, 1 };
            this.GenerateOldMessages();
            this.GenerateMessages();
            this.LoadMoreBehavior = LoadMoreOption.Auto;
            LoadMoreCommand = new Command<object>(LoadMoreItems, CanLoadMoreItems);
        }
        #endregion

        #region Generate Messages

        /// <summary>
        /// Generates new messages.
        /// </summary>
        private void GenerateMessages()
        {
            for (int i = 10; i < 20; i++)
            {
                var m = new TextMessage()
                {
                    Author = this.AuthorsCollection![this.authorArray[i]],
                    Text = this.messageList![i]
                };
                this.messages.Add(m);
            }
        }

        #endregion

        #region Generate Old Messages

        /// <summary>
        /// Generates old messages.
        /// </summary>
        private void GenerateOldMessages()
        {
            for (int i = 0; i < 10; i++)
            {
                var m = new TextMessage()
                {
                    Author = this.AuthorsCollection![this.authorArray[i]],
                    Text = this.messageList![i]
                };
                this.oldMessages.Add(m);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes authors collection.
        /// </summary>
        private void InitializeAuthors()
        {
            this.AuthorsCollection = new ObservableCollection<Author>();
            this.AuthorsCollection.Add(new Author() { Name = "Nancy", Avatar = "peoplecircle16.png" });
            this.AuthorsCollection.Add(new Author() { Name = "Andrea", Avatar = "andrea.png" });
            this.AuthorsCollection.Add(new Author() { Name = "Harrison", Avatar = "harrison.png" });
            this.AuthorsCollection.Add(new Author() { Name = "Margaret", Avatar = "margaret.png" });
            this.AuthorsCollection.Add(new Author() { Name = "Steven", Avatar = "steven.png" });
            this.AuthorsCollection.Add(new Author() { Name = "Michael", Avatar = "peoplecircle23.png" });
        }

        /// <summary>
        /// Initializes message list.
        /// </summary>
        private void InitializeMessageList()
        {
            this.messageList = new List<string>
            {
                "Good morning.",
                "Good morning.",
                "Morning.",
                "Is this all of us?",
                "Yes, Tammy has a couple of vid conferences, so I’m going to email her a summary when we’re done.",
                 "Ok.",
                 "Did everyone get the email with Jamie’s registry link?",
                 "Yes. The link worked.",
                 "There’s some cute stuff on there. You wanted to go in together on one of the more expensive things?",
                "Yeah. Did you see that stroller? I was looking at it.",
                "Here 👇",
                "Oh, yeah. I saw that. It’s very cool that it turns into a car seat.",
                "It has really good reviews, so I assume they did a lot of research before picking it.",
                "They want the teal version, right?",
                "That’s what comes up when I click the registry link, so I assume so.",
                "“Lake Blue”.",
                "Looks teal to me.",
                 "It’s better than just plain black, in any case.",
                 "True.",
                 "How many of us are pooling for it?",
                "Tammy is a probably. Neal gave me a max of $30, and Son just said sure. I think he just doesn’t want to shop for baby things. So at least 5, probably 6.",
                 "😂 That’s so Son.",
                 "So that puts us at between $26 and $31 per person, before tax. That’s not too bad.",
                "Yeah. I mean, I’m sure Jamie would love toys and books, but I figure there should be at least something from her list at the shower.",
                "I’m ok with it.",
                "Yeah, me, too.",
                "Great. I’ll just send an email to Tammy for when she gets out of her calls.",
                "Do you have Prime?",
                "Yes! Although I think I’d get free shipping at that price point, anyway.",
                "It’s nice that it’s on sale, too.",
                "Yeah, that’s why I want to get it ordered today. Also so I have time to inspect it a bit, make sure it works.",
                "Are we wrapping it at all?",
                "I have this giant bow in my closet I can put on it. I don’t even remember where it came from. It’s purple.",
                "Gender-neutral. Yay.",
                "They know the gender, though, right?",
                "Yes. It’s a girl. Jamie’s not big on surprises. She likes purple, though.",
                "That works, then.",
                "Do you want to do our own cards?",
                "You know Son won’t get one.",
                "We can just tell them who the stroller’s from.",
                "Yeah. We should keep a list at the party of who gave what anyway.",
                "I think that’s customary. For thank you cards.",
                "So yes, we’ll all do our own cards.",
                "Is the party in the conference room or the break room?",
                "Conference room. Mae has it booked for us.",
                "That’s good. Cleaner and easier to hide gifts in.",
                "Easier to decorate without getting in people’s way, too.",
                "You don’t want people spilling their lunches all over the tablecloth?",
                "That would be bad.",
                "😂 😂 😂",
                "I think Mae’s going to do the decorating in the morning. Aisha authorized a small budget for balloons and stuff.",
                "Tell her to let us know if she needs any help. I should have some downtime around 10:30.",
                "Ok, thanks. I will.",
                "Ack. I wish I could help, but I’ve got vid calls myself scheduled all that morning.",
                "No problem. I think there’ll be plenty of people.",
                "I’ll email everyone when Tammy gets back to me about the gift.",
                "Ok, that sounds good.",
                "Yes. I’ll see you two later.",
                "Thanks for meeting with me. Bye.",
                "Bye 👋 👋 👋"
            };
        }

        private bool CanLoadMoreItems(object obj)
        {

            if (this.OldMessages.Count > 0)
            {
                return true;
            }
            else
            {
                this.LoadMoreBehavior = LoadMoreOption.None;
                IsBusy = false;
                return false;
            }
        }

        private async void LoadMoreItems(object obj)
        {
            try
            {
                // Set is busy as true to show the busy indicator
                this.IsBusy = true;
                await Task.Delay(2000);
                LoadMoreMessages();
            }
            catch { }
            finally
            {
                // Set is busy as false to hide the busy indicator
                IsBusy = false;
            }
        }

        private void LoadMoreMessages()
        {
            for (int i = 1; i <= 5; i++)
            {
                var oldMessage = this.OldMessages[this.OldMessages.Count - 1];
                this.Messages!.Insert(0, oldMessage);
                this.OldMessages.Remove(oldMessage);
            }
        }

        #endregion

        #region PropertyChange

        /// <summary>
        /// Property changed handler.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Occurs when property is changed.
        /// </summary>
        /// <param name="propName">changed property name</param>
        public void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion

    }
}
