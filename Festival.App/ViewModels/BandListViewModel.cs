using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.App.ViewModels.Interfaces;
using Festival.BL.Facades;
using Festival.App.Services;
using Festival.App.Commands;
using Festival.BL.Models;
using Festival.App.Messages;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Festival.App.ViewModels
{
    public class BandListViewModel : ViewModelBase, IBandListViewModel
    {

        private readonly BandFacade _bandFacade;
        private readonly IMediator _mediator;

        public BandListViewModel(
            BandFacade bandFacade, 
            IMediator mediator)
        {
            _bandFacade = bandFacade;
            _mediator = mediator;

            BandSelectedCommand = new RelayCommand<BandListModel>(BandSelected);
            BandNewCommand = new RelayCommand(BandNew);

            mediator.Register<UpdateMessage<BandDetailModel>>(BandUpdated);
            mediator.Register<DeleteMessage<BandDetailModel>>(BandDeleted);
        }

        public ObservableCollection<BandListModel> BandsCollection { get; set; } = new ObservableCollection<BandListModel>() {};

        public ICommand BandSelectedCommand { get; }
        public ICommand BandNewCommand { get; }

        private void BandNew() => _mediator.Send(new NewMessage<BandDetailModel>());

        private void BandSelected(BandListModel band) => _mediator.Send(new SelectedMessage<BandDetailModel> { Id = band.Id });

        private void BandUpdated(UpdateMessage<BandDetailModel> _) => Load();

        private void BandDeleted(DeleteMessage<BandDetailModel> _) => Load();

        public void Load()
        {
            BandsCollection.Clear();
            var bands = _bandFacade.GetAllList();
            foreach (var band in bands)
            {
                BandsCollection.Add(band);
            }
        }
    }
}
