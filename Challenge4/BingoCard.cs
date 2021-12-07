using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge4
{
    public class BingoCard
    {
        private readonly int _cardSize;
        private int[,] _cardNumbers;
        private int[,] _foundNumbers;

        public bool IsWinner { get; private set; }

        public BingoCard(int cardSize, IReadOnlyCollection<string> cardNumberLines)
        {
            if (cardSize <= 0)
            {
                throw new InvalidOperationException("Invalid CardSize");
            }
            _cardSize = cardSize;

            InitializeCard(cardNumberLines);
        }

        public void SetNumber(int drawnNumber)
        {
            for (int row = 0; row < _cardSize; row++)
            {
                for (int column = 0; column < _cardSize; column++)
                {
                    if (_cardNumbers[row, column] == drawnNumber)
                    {
                        _foundNumbers[row, column] = 1;

                        if (CheckBingoForRowAndColumn(row, column))
                        {
                            IsWinner = true;
                            return;
                        }
                    }
                }
            }
        }

        private bool CheckBingoForRowAndColumn(int row, int column)
        {
            return _foundNumbers.GetRow(row).Sum() == _cardSize ||
                   _foundNumbers.GetColumn(column).Sum() == _cardSize;
        }

        private void InitializeCard(IReadOnlyCollection<string> cardNumberLines)
        {
            _foundNumbers = new int[_cardSize, _cardSize];
            _cardNumbers = new int[_cardSize, _cardSize];
            for (int row = 0; row < _cardSize; row++)
            {
                var cardColumnNumbers = cardNumberLines.ElementAt(row).Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for (int column = 0; column < _cardSize; column++)
                {
                    _cardNumbers[row, column] = int.Parse(cardColumnNumbers[column]);
                    _foundNumbers[row, column] = 0;
                }
            }
        }

        public int GetWinningRowValue()
        {
            var calculatedValue = 0;

            for (int row = 0; row < _cardSize; row++)
            {
                for (int column = 0; column < _cardSize; column++)
                {
                    if (_foundNumbers[row, column] == 0)
                    {
                        calculatedValue += _cardNumbers[row, column];
                    }
                }
            }

            return calculatedValue;
        }
    }
}