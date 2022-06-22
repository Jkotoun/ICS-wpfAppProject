using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Festival.App.Commands;
using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.ViewModels.Interfaces;
using Festival.BL.Facades;
using Festival.BL.Models;

namespace Festival.App.ViewModels
{
    public class BandDetailViewModel: ViewModelBase, IBandDetailViewModel
    {
        private readonly BandFacade _BandFacade;
        private readonly IMediator _mediator;

        public BandDetailViewModel(
            BandFacade bandFacade,
            IMediator mediator)
        {
            _BandFacade = bandFacade;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete, CanDelete);

            mediator.Register<SelectedMessage<BandDetailModel>>(BandSelected);
            mediator.Register<NewMessage<BandDetailModel>>(BandNew);
        }

        public BandDetailModel? Model { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void BandSelected(SelectedMessage<BandDetailModel> band) => Load(band.Id);
        private void BandNew(NewMessage<BandDetailModel> _) => Load(Guid.Empty);


        public void Load(Guid id)
        {
           Model = _BandFacade.GetById(id) ?? new BandDetailModel();
           OnPropertyChanged(nameof(Model));
        }

        public void Save()
        {
            _BandFacade.InsertOrUpdate(Model);
            _mediator.Send(new UpdateMessage<BandDetailModel> {Model = Model});
        }

        private bool CanSave() => 
            Model != null
            && !string.IsNullOrWhiteSpace(Model.Name);

        public void Delete()
        {
            try
            {
                _BandFacade.Delete(Model.Id);
            }
            catch
            {
                MessageBox.Show("Deleting failed!");
            }

            _mediator.Send(new DeleteMessage<BandDetailModel>
            {
                Model = Model
            });
            Load(Guid.Empty);
        }
        private bool CanDelete()
        {
            return Model != null && Model.Id != Guid.Empty;
        }
    }
}
