using ConnectionLost.Core;
using ConnectionLost.Models;
using ConnectionLost.Views;
using System;
using UnityEngine;


namespace ConnectionLost.Controllers
{
    internal sealed class CellController : IDisposable
    {
        private readonly CellModel _model;
        private readonly CellView _view;

        public CellController(CellModel model, CellView view)
        {
            _model = model;
            _view = view;

            _model.OnCellStateChanged += UpdateVisual;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            Debug.Log("UpdateCellState");

            if (_model.CurrentState == CellStates.Empty)
                _view.SetEmpty();

            else if (_model.IsBlocked())
            {
                _view.SetBlocked();
            }

            else
            {
                switch (_model.CurrentState)
                {
                    case CellStates.Opened:
                        _view.SetOpened();
                        break;

                    case CellStates.Closed:
                        _view.SetClosed();
                        break;

                    case CellStates.HaveContent:
                        _view.SetOpened();
                        break;
                }
            }
        }

        public ClickResult ClickOnCell(PlayerController player)
        {
            var result = new ClickResult();

            if (!_model.IsBlocked())
            {
                if (_model.CurrentState == CellStates.Opened)
                {
                    OpenCell(result);
                }

                else if (_model.CurrentState == CellStates.HaveContent)
                {
                    HandleContent(player, result);
                }
            }

            return result;
        }

        private void OpenCell(ClickResult result)
        {
            _model.SetNewState(_model.CellContent != null ? CellStates.HaveContent : CellStates.Empty);


            foreach (var neighbor in _model.GetNeighboursList())
            {
                neighbor.SetNewState(CellStates.Opened);
                if (_model.CellContent is { IsBlock: true })
                    neighbor.SetBlock();
            }

            result.CellContent = _model.CellContent;
            result.NeedUpdate = true;
        }

        private void HandleContent(PlayerController player, ClickResult result)
        {
            if (_model.CellContent is EnemyBase enemy)
            {
                player.AttackEnemy(enemy);

                if (enemy.Hp.Value <= 0)
                {
                    foreach (var neighbor in _model.GetNeighboursList())
                    {
                        neighbor.SetUnblock();
                    }

                    _model.CellContent = null;
                    _model.SetNewState(CellStates.Empty);
                    result.NeedUpdate = true;
                }

            }

            result.CellContent = _model.CellContent;
        }


        public void Dispose()
        {
            _model.OnCellStateChanged -= UpdateVisual;
            UnityEngine.Object.Destroy(_view.gameObject);
        }
    }
}
