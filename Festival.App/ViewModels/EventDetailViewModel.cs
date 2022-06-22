using Festival.App.Commands;
using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.ViewModels.Interfaces;
using Festival.BL.Facades;
using Festival.BL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festival.App.ViewModels
{
    public class EventDetailViewModel : ViewModelBase, IEventDetailViewModel, INotifyPropertyChanged
    {
        private readonly BandFacade _bandFacade;
        private readonly StageFacade _stageFacade;
        private readonly EventFacade _eventFacade;
        private readonly IMediator _mediator;

        public EventDetailViewModel(
            BandFacade bandFacade,
            StageFacade stageFacade,
            EventFacade eventFacade,
            IMediator mediator)
        {
            _bandFacade = bandFacade;
            _stageFacade = stageFacade;
            _eventFacade = eventFacade;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete, CanDelete);

            mediator.Register<SelectedMessage<EventDetailModel>>(EventSelected);
            mediator.Register<NewMessage<EventDetailModel>>(EventNew);
        }

        public EventDetailModel SelectedEvent
        {
            get; set;
        }
        public BandDetailModel SelectedBand
        {
            get; set;
        }
        public StageDetailModel SelectedStage
        {
            get; set;
        }
        public ObservableCollection<BandDetailModel> BandsCollection { get; set; } = new ObservableCollection<BandDetailModel>();
        public ObservableCollection<StageDetailModel> StagesCollection { get; set; } = new ObservableCollection<StageDetailModel>();
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void EventSelected(SelectedMessage<EventDetailModel> ev) => Load(ev.Id);
        private void EventNew(NewMessage<EventDetailModel> _) => Load(Guid.Empty);

        public void Load(Guid id)
        {
            SelectedEvent = _eventFacade.GetById(id) ?? new EventDetailModel();

            StagesCollection.Clear();
            var Stages = _stageFacade.GetAllDetail();
            foreach (var Stage in Stages)
            {
                StagesCollection.Add(Stage);
            }

            BandsCollection.Clear();
            var Bands = _bandFacade.GetAllDetail();
            foreach (var Band in Bands)
            {
                BandsCollection.Add(Band);
            }
            SelectedBand = BandsCollection.FirstOrDefault(a => a.Id == SelectedEvent.Band?.Id);
            SelectedStage = StagesCollection.FirstOrDefault(a => a.Id == SelectedEvent.Stage?.Id);

            OnPropertyChanged(nameof(SelectedBand));
            OnPropertyChanged(nameof(SelectedEvent));
            OnPropertyChanged(nameof(SelectedStage));
        }

        public void Save()
        {
            if (SelectedEvent == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }
            SelectedEvent.BandId = SelectedBand.Id;
            SelectedEvent.Band = null;
            SelectedEvent.StageId = SelectedStage.Id;
            SelectedEvent.Stage = null;
            try
            {
                _eventFacade.InsertOrUpdate(SelectedEvent);
                _mediator.Send(new UpdateMessage<EventDetailModel> { Model = SelectedEvent });
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
            
            
        }

        private bool CanSave()
        {
            //OnPropertyChanged();
            return SelectedEvent != null
            && SelectedBand != null
            && SelectedStage != null
            && SelectedEvent.StartTime != null
            && SelectedEvent.EndTime != null;
        }


        public void Delete()
        {

            if (SelectedEvent.Id != Guid.Empty)
            {
                try
                {
                    _eventFacade.Delete(SelectedEvent.Id);
                }
                catch
                {
                    MessageBox.Show("Deleting failed!");
                }

                _mediator.Send(new DeleteMessage<EventDetailModel>
                {
                    Model = SelectedEvent
                });
                Load(Guid.Empty);
            }
        }

        private bool CanDelete()
        {
            return SelectedEvent != null && SelectedEvent.Id != Guid.Empty;
        }
    }
}
