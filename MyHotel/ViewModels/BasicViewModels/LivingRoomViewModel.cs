﻿using Microsoft.Win32;
using MyHotel.Core;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MyHotel
{
    public class LivingRoomViewModel : ValidationObservableModel
    {
        private const string DefaultFolder = @"Photo\Rooms";

        private readonly IShellViewModel _shell;

        private LivingRoom _model;
        private string _description;
        private int _floor;
        private int _cost;
        private bool _isEditMode;
        private bool _state;
        private string _newServiceStr;
        private string _selectPhotoPath;
        private string _selectedPhoto;

        public string NumberStr => $"Room №{_model.Id}";

        public string CostStr => $"Cost: {Cost}$";

        public string FloorStr => $"Floor: {Floor}";


        public ObservableCollection<string> Photos { get; set; }

        public ObservableCollection<string> Services { get; set; }


        public DelegateCommand EditModeOn { get; set; }

        public DelegateCommand ViewModeOn { get; set; }

        public DelegateCommand ServiceAdd { get; set; }

        public DelegateCommand ServiceRemove { get; set; }

        public DelegateCommand PhotoAdd { get; set; }

        public DelegateCommand PhotoRemove { get; set; }

        public DelegateCommand OpenPhoto { get; set; }

        public ICommand ReserveCommand { get; set; }


        public LivingRoomViewModel(IShellViewModel shell)
        {
            _shell = shell;

            EditModeOn = new DelegateCommand(EditModeOnDelegate);
            ViewModeOn = new DelegateCommand(ViewModeOnDelegate);

            OpenPhoto = new DelegateCommand(OpenPhotoDialog);
            PhotoAdd = new DelegateCommand(AddPhotoDelegate);
            PhotoRemove = new DelegateCommand(RemovePhotoDelegate);

            ReserveCommand = new DelegateCommand(ReserveCommandDelegate);
        }

        public LivingRoomViewModel(LivingRoom room, IShellViewModel shell) : this(shell)
        {
            AttachModel(room);

            SelectedPhoto = Photos?.FirstOrDefault();
        }

        public void AttachModel(LivingRoom room)
        {
            _model = room;
            Floor = room.Floor;
            Cost = room.Cost;
            Description = room.Descriptions;
            State = room.Status;
            Id = room.Id;

            room.UpdateCollections();

            UpdatePhoto();
            RefreshModel();
        }

        public void RefreshModel()
        {
            NotifyPropertyChanged(() => Floor);
            NotifyPropertyChanged(() => Cost);
            NotifyPropertyChanged(() => Description);
            NotifyPropertyChanged(() => State);

            IsEditMode = false;
        }

        public bool IsViewMode { get; set; }

        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                IsViewMode = !value;

                NotifyPropertyChanged(() => IsEditMode);
                NotifyPropertyChanged(() => IsViewMode);
            }
        }

        public int Id { get; set; }

        public int Floor
        {
            get => _floor;
            set
            {
                _floor = value;
                NotifyPropertyChanged(() => Floor);
            }
        }

        public bool State
        {
            get => _state;
            set
            {
                _state = value;
                NotifyPropertyChanged(() => State);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged(() => Description);
            }
        }

        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                NotifyPropertyChanged(() => Cost);
                NotifyPropertyChanged(() => CostStr);
            }
        }

        public string NewServiceStr
        {
            get => _newServiceStr;
            set
            {
                _newServiceStr = value;
                NotifyPropertyChanged(() => NewServiceStr);
            }
        }

        public string SelectPhotoPath
        {
            get => _selectPhotoPath;
            set
            {
                _selectPhotoPath = Path.GetFileName(value);
                NotifyPropertyChanged(() => SelectPhotoPath);
            }
        }

        public string SelectedPhoto
        {
            get => _selectedPhoto;
            set
            {
                _selectedPhoto = value;
                NotifyPropertyChanged(() => SelectedPhoto);
            }
        }

        public string MainPhoto => Photos.Count > 0 ? Convert(Photos.First()) : null;

        public UserViewModel CurrentUser => _shell.CurrentUser;

        private void EditModeOnDelegate(object o)
        {
            IsEditMode = true;
        }

        private void ViewModeOnDelegate(object o)
        {
            IsEditMode = false;

            _shell.CoreManager.RoomManager.UpdateModel(_model, Cost, Description, State);
        }

        private void AddServiceDelegate(object o)
        {
            //if (!string.IsNullOrEmpty)
        }

        private void AddPhotoDelegate(object o)
        {
            if (!string.IsNullOrEmpty(SelectPhotoPath))
            {
                var ok = _shell.CoreManager.RoomManager.AddPhoto(_model, SelectPhotoPath);

                if (ok)
                    UpdatePhoto();

                SelectPhotoPath = string.Empty;
            }
        }

        private void RemovePhotoDelegate(object o)
        {
            if (SelectPhotoPath != null)
            {
                var ok = _shell.CoreManager.RoomManager.RemovePhoto(_model, SelectPhotoPath);

                if (ok)
                    UpdatePhoto();
            }
        }

        private void OpenPhotoDialog(object o)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                SelectPhotoPath = openFileDialog.FileName;
            }
        }

        private void UpdatePhoto()
        {
            Photos = new ObservableCollection<string>(_model.Photo.Select(Convert));

            NotifyPropertyChanged(() => Photos);
            NotifyPropertyChanged(() => MainPhoto);
        }

        private string Convert(string path) => Path.Combine(Environment.CurrentDirectory, DefaultFolder, path);

        private void ReserveCommandDelegate(object o)
        {
            var viewModel = new ReservationDialogViewModel(_shell, _model.Id);

            var dialog = new ReservationDialog()
            {
                DataContext = viewModel,
                Owner = Application.Current.MainWindow
            };
            dialog.ShowDialog();
        }
    }
}
