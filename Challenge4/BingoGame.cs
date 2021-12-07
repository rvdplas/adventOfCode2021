using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge4
{
    public class BingoGame
    {
        private const int CardSize = 5;

        private List<int> _numbersToDraw;
        private readonly List<BingoCard> _bingoCards;

        public BingoGame(IReadOnlyCollection<string> lines)
        {
            _numbersToDraw = new List<int>();
            _bingoCards = new List<BingoCard>();

            ParseDrawnNumbers(lines.ElementAt(0));
            ProcessBingCards(lines.Skip(2).ToList());
        }

        private void ProcessBingCards(IReadOnlyCollection<string> bingoCardLines)
        {
            var currentRow = 0;

            for (int i = 0; i <= bingoCardLines.Count; i++)
            {
                var skipCards = currentRow * (CardSize + 1);

                var cardLines = bingoCardLines.Skip(skipCards).Take(CardSize).ToList();
                _bingoCards.Add(new BingoCard(CardSize, cardLines));

                currentRow++;
                i = currentRow * (CardSize + 1);
            }
        }

        private void ParseDrawnNumbers(string drawnNumbersLine)
        {
            _numbersToDraw = drawnNumbersLine.Split(",").Select(int.Parse).ToList();
        }

        public int DrawNumbers()
        {
            var drawnNumbers = 0;

            foreach (var drawnNumber in _numbersToDraw)
            {
                foreach (var bingoCard in _bingoCards)
                {
                    bingoCard.SetNumber(drawnNumber);
                    if (drawnNumbers < CardSize)
                    {
                        // no need to check the first 4 numbers drawn, never a winner
                        continue;
                    }

                    if (bingoCard.IsWinner)
                    {
                        return drawnNumber * bingoCard.GetWinningRowValue();
                    }
                }

                drawnNumbers++;
            }

            throw new InvalidOperationException("Could not find winning card");
        }

        public int FindLastWinningBoard()
        {
            var drawnNumbers = 0;
            var foundWinners = 0;
            var numberOfBoards = _bingoCards.Count;

            foreach (var drawnNumber in _numbersToDraw)
            {
                foreach (var bingoCard in _bingoCards.Where(x => !x.IsWinner))
                {
                    bingoCard.SetNumber(drawnNumber);
                    if (drawnNumbers < CardSize)
                    {
                        // no need to check the first 4 numbers drawn, never a winner
                        continue;
                    }

                    if (bingoCard.IsWinner)
                    {
                        foundWinners++;
                    }

                    if (bingoCard.IsWinner && foundWinners == numberOfBoards)
                    {
                        return drawnNumber * bingoCard.GetWinningRowValue();
                    }
                }

                drawnNumbers++;
            }

            throw new InvalidOperationException("Could not find winning card");
        }
    }
}