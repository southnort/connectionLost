using ConnectionLost.Models;
using ConnectionLost.Views;
using System;

namespace ConnectionLost.Controllers
{
    internal sealed class PlayerController:IDisposable
    {
        private readonly PlayerModel _model;
        private readonly PlayerStateView _playerStateView;





        private void OnHpChanged(float hp)
        {

        }

        private void OnDmgChanged(float dmg) 
        {
        
        }


        public void Dispose()
        {
            UnityEngine.Object.Destroy(_playerStateView.gameObject);
            _model.Hp.OnChange -= OnHpChanged;
            _model.Damage.OnChange-= OnDmgChanged;
        }
    }
}
