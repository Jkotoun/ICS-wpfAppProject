using Festival.App.Commands;
using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.ViewModels.Interfaces;
using Festival.BL.Facades;
using Festival.BL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festival.App.ViewModels
{
    public class EventListViewModel : ViewModelBase, IEventListViewModel
    {
        private readonly StageFacade _stageFacade;
        private readonly IMediator _mediator;
        public EventListViewModel(
            StageFacade stageFacade,
            IMediator mediator)
        {
            _stageFacade = stageFacade;
            _mediator = mediator;

            EventSelectedCommand = new RelayCommand<EventDetailModel>(EventSelected);
            EventNewCommand = new RelayCommand(EventNew);

            mediator.Register<UpdateMessage<EventDetailModel>>(EventUpdated);
            mediator.Register<DeleteMessage<EventDetailModel>>(EventDeleted);
        }

        public ObservableCollection<StageDetailModel> StageCollection { get; set; } = new ObservableCollection<StageDetailModel>();

        public ICommand EventSelectedCommand { get; }
        public ICommand EventNewCommand { get; }

        private void EventNew() =>_mediator.Send(new NewMessage<EventDetailModel>());

        private void EventSelected(EventDetailModel ev) => _mediator.Send(new SelectedMessage<EventDetailModel> { Id = ev.Id });

        private void EventUpdated(UpdateMessage<EventDetailModel> _) => Load();

        private void EventDeleted(DeleteMessage<EventDetailModel> _) => Load();

        public void Load()
        {
            StageCollection.Clear();
            var Stages = _stageFacade.GetAllDetail();
            foreach (var stage in Stages)
            {
                StageCollection.Add(stage);
            }
        }
    }
}
