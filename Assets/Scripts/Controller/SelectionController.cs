using UnityEngine;

namespace Geekbrains
{
    public sealed class SelectionController : BaseController, IExecute, IInitialization
    {
        private SelectionUi _selectionUi;
        private SelectionModel _selectionModel;
        private GameObject _gameObject;
        private bool _isUsing;
        private BaseMakeUsingModel _usingModel;
        public void Initialization()
        {
            _selectionUi = Object.FindObjectOfType<SelectionUi>();
            _selectionModel = Object.FindObjectOfType<SelectionModel>();
        }

        public override void On()
        {
            if (IsActive) return;
            base.On();
            _selectionUi.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _selectionUi.SetActive(false);
        }

        public void Execute()
        {
            if (!IsActive)
                return;
            _gameObject = _selectionModel.FindObject();
            if (_gameObject == null)
            {
                _selectionUi.Text = string.Empty;
                return;
            }

            _usingModel = _gameObject.GetComponent<BaseMakeUsingModel>();
            if (_usingModel != null)
            {
                _selectionUi.Text = string.Format("{0} (E)", _gameObject.name);
                _isUsing = true;
            }
            else
            {
                _isUsing = false;
                _selectionUi.Text = _gameObject.name;
            }
            
        }

        public void makeUse()
        {
            if (_isUsing)
            {
                _usingModel.MakeUsing();
                _gameObject.SetActive(false);
            }
            
        }

    }
}
