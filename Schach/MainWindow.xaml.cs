using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Schach.MainWindow;
using static System.Net.Mime.MediaTypeNames;

namespace Schach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Notation> notations = new ObservableCollection<Notation>();
        #region Chess Boards
        List<List<string>> chessBoard = new List<List<string>>();
        List<List<string>> currentChessBoard = new List<List<string>>();
        List<List<string>> originalChessBoard = new List<List<string>>();
        #endregion
        #region Dictionaries
        public Dictionary<string, Brush> borderColors = new Dictionary<string, Brush>
        {
            { "A8", new SolidColorBrush(Colors.Beige) },
            { "B8", new SolidColorBrush(Colors.SaddleBrown) },
            { "C8", new SolidColorBrush(Colors.Beige) },
            { "D8", new SolidColorBrush(Colors.SaddleBrown) },
            { "E8", new SolidColorBrush(Colors.Beige) },
            { "F8", new SolidColorBrush(Colors.SaddleBrown) },
            { "G8", new SolidColorBrush(Colors.Beige) },
            { "H8", new SolidColorBrush(Colors.SaddleBrown) },

            { "A7", new SolidColorBrush(Colors.SaddleBrown) },
            { "B7", new SolidColorBrush(Colors.Beige) },
            { "C7", new SolidColorBrush(Colors.SaddleBrown) },
            { "D7", new SolidColorBrush(Colors.Beige) },
            { "E7", new SolidColorBrush(Colors.SaddleBrown) },
            { "F7", new SolidColorBrush(Colors.Beige) },
            { "G7", new SolidColorBrush(Colors.SaddleBrown) },
            { "H7", new SolidColorBrush(Colors.Beige) },

            { "A6", new SolidColorBrush(Colors.Beige) },
            { "B6", new SolidColorBrush(Colors.SaddleBrown) },
            { "C6", new SolidColorBrush(Colors.Beige) },
            { "D6", new SolidColorBrush(Colors.SaddleBrown) },
            { "E6", new SolidColorBrush(Colors.Beige) },
            { "F6", new SolidColorBrush(Colors.SaddleBrown) },
            { "G6", new SolidColorBrush(Colors.Beige) },
            { "H6", new SolidColorBrush(Colors.SaddleBrown) },

            { "A5", new SolidColorBrush(Colors.SaddleBrown) },
            { "B5", new SolidColorBrush(Colors.Beige) },
            { "C5", new SolidColorBrush(Colors.SaddleBrown) },
            { "D5", new SolidColorBrush(Colors.Beige) },
            { "E5", new SolidColorBrush(Colors.SaddleBrown) },
            { "F5", new SolidColorBrush(Colors.Beige) },
            { "G5", new SolidColorBrush(Colors.SaddleBrown) },
            { "H5", new SolidColorBrush(Colors.Beige) },

            { "A4", new SolidColorBrush(Colors.Beige) },
            { "B4", new SolidColorBrush(Colors.SaddleBrown) },
            { "C4", new SolidColorBrush(Colors.Beige) },
            { "D4", new SolidColorBrush(Colors.SaddleBrown) },
            { "E4", new SolidColorBrush(Colors.Beige) },
            { "F4", new SolidColorBrush(Colors.SaddleBrown) },
            { "G4", new SolidColorBrush(Colors.Beige) },
            { "H4", new SolidColorBrush(Colors.SaddleBrown) },

            { "A3", new SolidColorBrush(Colors.SaddleBrown) },
            { "B3", new SolidColorBrush(Colors.Beige) },
            { "C3", new SolidColorBrush(Colors.SaddleBrown) },
            { "D3", new SolidColorBrush(Colors.Beige) },
            { "E3", new SolidColorBrush(Colors.SaddleBrown) },
            { "F3", new SolidColorBrush(Colors.Beige) },
            { "G3", new SolidColorBrush(Colors.SaddleBrown) },
            { "H3", new SolidColorBrush(Colors.Beige) },

            { "A2", new SolidColorBrush(Colors.Beige) },
            { "B2", new SolidColorBrush(Colors.SaddleBrown) },
            { "C2", new SolidColorBrush(Colors.Beige) },
            { "D2", new SolidColorBrush(Colors.SaddleBrown) },
            { "E2", new SolidColorBrush(Colors.Beige) },
            { "F2", new SolidColorBrush(Colors.SaddleBrown) },
            { "G2", new SolidColorBrush(Colors.Beige) },
            { "H2", new SolidColorBrush(Colors.SaddleBrown) },

            { "A1", new SolidColorBrush(Colors.SaddleBrown) },
            { "B1", new SolidColorBrush(Colors.Beige) },
            { "C1", new SolidColorBrush(Colors.SaddleBrown) },
            { "D1", new SolidColorBrush(Colors.Beige) },
            { "E1", new SolidColorBrush(Colors.SaddleBrown) },
            { "F1", new SolidColorBrush(Colors.Beige) },
            { "G1", new SolidColorBrush(Colors.SaddleBrown) },
            { "H1", new SolidColorBrush(Colors.Beige) }
        };
        public Dictionary<ChessColor, bool> isInCheck = new Dictionary<ChessColor, bool>
        {
             { ChessColor.White, false },
             { ChessColor.Black, false },
        };
        public Dictionary<ChessPiece, string> ChessPieceAbbr = new Dictionary<ChessPiece, string>
        {
            {ChessPiece.Pawn, ""},
            {ChessPiece.Knight, "N"},
            {ChessPiece.Bishop, "B"},
            {ChessPiece.Queen, "Q"},
            {ChessPiece.King, "K"},
            {ChessPiece.Rook, "R"},
        };
        #endregion
        #region Dynamic variables
        List<Move> possibleMoves = new List<Move>();
        ChessColor currentTurn = ChessColor.White;
        private string selectedField = "";
        private int selectedCounter = 0;
        #endregion
        #region enums
        public enum ChessColor
        {
            White,
            Black
        }
        public enum ChessPiece
        {
            Pawn,
            Rook,
            Bishop,
            Knight,
            Queen,
            King
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            FillChessBoard();
            FillCurrentChessBoard();
            Notations_lv.ItemsSource = notations;
        }
        private bool IsCheck(Move move)
        {
            var children = GetAllChildren(ChessBoard);
            List<string> chessFieldsName = new List<string>();
            List<object> chessFields = new List<object>();
            List<Piece> pieces = new List<Piece>();
            Piece checkableKing = new Piece(ChessPiece.King, ChessColor.Black);
            List<List<Move>> moves = new List<List<Move>>();
            string oldSelectedField = selectedField;
            foreach (var child in children)
                if (child is Border border)
                {
                    chessFieldsName.Add(border.Name);
                    chessFields.Add(border);
                }
            int startIndex = chessFieldsName.IndexOf(move.endField);
            Piece startPiece = IdentifyChessPiece(chessFields[startIndex]);
            if (startPiece.color == ChessColor.Black)
                checkableKing = new Piece(ChessPiece.King, ChessColor.White);
            for(int i = 0; i < chessFields.Count; i++)
            {
                Piece currentPiece = IdentifyChessPiece(chessFields[i]);
                if (currentPiece != null)
                {
                    if (currentPiece.color.ToString() == startPiece.color.ToString())
                    {
                        pieces.Add(currentPiece);
                        selectedField = chessFieldsName[i];
                        moves.Add(GetMoves(chessFields[i]));
                    }
                }
            }
            for(int i = 0; i < moves.Count; i++)
            {
                for(int j = 0; j < moves[i].Count; j++)
                {
                    int indexRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(moves[i][j].endField)));
                    int indexColumn = chessBoard[indexRow].IndexOf(moves[i][j].endField);
                    if(IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]) != null)
                        if (IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).piece == checkableKing.piece && IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color == checkableKing.color)
                        return true;
                }
            }
            selectedField = oldSelectedField;
            return false;
        }
        private bool IsChecked(Move move)
        {
            var children = GetAllChildren(ChessBoard);
            List<string> chessFieldsName = new List<string>();
            List<object> chessFields = new List<object>();
            List<Piece> pieces = new List<Piece>();
            Piece checkableKing = new Piece(ChessPiece.King, ChessColor.Black);
            List<List<Move>> moves = new List<List<Move>>();
            string oldSelectedField = selectedField;
            foreach (var child in children)
                if (child is Border border)
                {
                    chessFieldsName.Add(border.Name);
                    chessFields.Add(border);
                }
            int startIndex = chessFieldsName.IndexOf(move.endField);
            Piece startPiece = IdentifyChessPiece(chessFields[startIndex]);
            if (startPiece.color == ChessColor.White)
                checkableKing = new Piece(ChessPiece.King, ChessColor.White);
            for(int i = 0; i < chessFields.Count; i++)
            {
                Piece currentPiece = IdentifyChessPiece(chessFields[i]);
                if (currentPiece != null)
                {
                    if (currentPiece.color.ToString() != startPiece.color.ToString())
                    {
                        pieces.Add(currentPiece);
                        selectedField = chessFieldsName[i];
                        moves.Add(GetMoves(chessFields[i]));
                    }
                }
            }
            for(int i = 0; i < moves.Count; i++)
            {
                for(int j = 0; j < moves[i].Count; j++)
                {
                    int indexRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(moves[i][j].endField)));
                    int indexColumn = chessBoard[indexRow].IndexOf(moves[i][j].endField);
                    if(IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]) != null)
                        if (IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).piece == checkableKing.piece && IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color == checkableKing.color)
                        return true;
                }
            }
            selectedField = oldSelectedField;
            return false;
        }
        private bool IsCheckmate(Move move)
        {
            bool checkmate = true;
            var children = GetAllChildren(ChessBoard);
            List<string> chessFieldsName = new List<string>();
            List<object> chessFields = new List<object>();
            List<List<string>> oldCurrentBoard = currentChessBoard.ToList();
            List<Piece> pieces = new List<Piece>();
            Piece checkableKing = new Piece(ChessPiece.King, ChessColor.Black);
            List<List<Move>> moves = new List<List<Move>>();
            string oldSelectedField = selectedField;
            foreach (var child in children)
                if (child is Border border)
                {
                    chessFieldsName.Add(border.Name);
                    chessFields.Add(border);
                }
            int startIndex = chessFieldsName.IndexOf(move.endField);
            Piece startPiece = IdentifyChessPiece(chessFields[startIndex]);
            if (startPiece.color == ChessColor.Black)
                checkableKing = new Piece(ChessPiece.King, ChessColor.White);
            for (int i = 0; i < chessFields.Count; i++)
            {
                Piece currentPiece = IdentifyChessPiece(chessFields[i]);
                if (currentPiece != null)
                {
                    if (currentPiece.color.ToString() != startPiece.color.ToString())
                    {
                        pieces.Add(currentPiece);
                        selectedField = chessFieldsName[i];
                        moves.Add(GetMoves(chessFields[i]));
                    }
                }
            }
            for(int i = 0; i < moves.Count; i++)
            {
                for(int j = 0; j < moves[i].Count; j++)
                {
                    MoveChessPiece(moves[i][j]);
                    if (!IsChecked(moves[i][j]))
                        checkmate = false;
                    if (notations.Count > 0)
                        ResetMoves();
                    else
                        ReverseMove(moves[i][j]);
                    if (!checkmate)
                    {
                        currentChessBoard = oldCurrentBoard.ToList();
                        return checkmate;
                    }
                }
            }
            selectedField = oldSelectedField;
            currentChessBoard = oldCurrentBoard.ToList();
            return checkmate;
        }

        private void ResetMoves()
        {
            if (notations.Count == 0)
                return;
            ResetChessBoard();
            for(int i = 0; i < notations.Count; i++)
            {
                Move move = new Move(notations[i].StartField, notations[i].EndField);
                MoveChessPiece(move);
            }
        }

        private void SelectedChessField(object sender)
        {
            Border border = (Border)sender;
            TextBlock textBlock = (TextBlock)border.Child;
            Move currentMove = new Move(selectedField, border.Name);
            if (string.IsNullOrEmpty(textBlock.Text) && selectedCounter == 0)
            {
                return;
            }
            if (border.Background.ToString() != borderColors[border.Name].ToString())
            {
                border.Background = borderColors[border.Name];
                selectedField = "";
                selectedCounter--;
            }
            else if(selectedCounter < 1)
            {
                if(IdentifyChessPiece(sender).color == currentTurn)
                {
                    border.Background = Brushes.Red;
                    selectedCounter++;
                    selectedField = border.Name;
                    possibleMoves = GetMoves(sender);
                }
            }
            else
            {
                for(int i = 0; i < possibleMoves.Count; i++)
                {

                    if(possibleMoves[i].endField == currentMove.endField)
                    {
                        if(currentMove.startField != currentMove.endField)
                        {
                            bool capture = true;
                            int indexRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(currentMove.endField)));
                            int indexColumn = chessBoard[indexRow].IndexOf(currentMove.endField);
                            if (currentChessBoard[indexRow][indexColumn] == "")
                                capture = false;
                            MoveChessPiece(sender);
                            bool check = IsCheck(currentMove);
                            bool isInCheck = IsChecked(currentMove);
                            bool isCheckMate = IsCheckmate(currentMove);
                            if (isInCheck)
                            {
                                ReverseMove(currentMove);
                                SwitchTurn();
                            }
                            if(!isInCheck && selectedCounter == 0)
                                notations.Add(new Notation(IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).piece, currentMove.startField, currentMove.endField, capture, check, isCheckMate, false, false, currentTurn));
                            if (isCheckMate)
                            {
                                MessageBox.Show(currentTurn + " won!");
                                SwitchTurn();
                                ResetChessBoard();
                                notations.Clear();
                            }
                            if (currentTurn == ChessColor.White)
                            {
                                if(check && !isInCheck)
                                {
                                    this.isInCheck[ChessColor.Black] = true;
                                }
                                currentTurn = ChessColor.Black;
                            }
                            else if (currentTurn == ChessColor.Black)
                            {
                                if (check && !isInCheck)
                                {
                                    this.isInCheck[ChessColor.White] = true;
                                }
                                currentTurn = ChessColor.White;
                            }
                        }
                    }
                }
            }
        }
        private void SwitchTurn()
        {
            if (currentTurn == ChessColor.White)
            {
                currentTurn = ChessColor.Black;
            }
            else if (currentTurn == ChessColor.Black)
            {
                currentTurn = ChessColor.White;
            }
        }
        private void ResetChessBoard()
        {
            List<DependencyObject> chessBoardBorders = GetAllChildren(ChessBoard);
            Border selectedBorder = (Border)chessBoardBorders.Find(DependencyObject => DependencyObject is Border border && border.Name == selectedField);
            selectedBorder.Background = borderColors[selectedField];
            selectedField = "";
            selectedCounter = 0;
            for (int i = 0; i < originalChessBoard.Count; i++)
            {
                for (int j = 0; j < originalChessBoard[i].Count; j++)
                {
                    List<DependencyObject> children = GetAllChildren(ChessBoard);
                    for (int k = 0; k < children.Count; k++)
                    {
                        if (children[k] is Border border)
                        {
                            if (border.Name == chessBoard[i][j])
                            {
                                TextBlock textBlock = (TextBlock)border.Child;
                                textBlock.Text = originalChessBoard[i][j];
                            }
                        }
                    }
                }
            }
            FillCurrentChessBoard();
            currentTurn = ChessColor.White;
        }
        private string PieceToCode(Piece piece)
        {
            string code = "";
            if (piece.piece == ChessPiece.Rook && piece.color == ChessColor.Black)
                code = "&#x265C;";
            else if (piece.piece == ChessPiece.Rook && piece.color == ChessColor.White)
                code = "&#x2656;";
            else if (piece.piece == ChessPiece.Knight && piece.color == ChessColor.Black)
                code = "&#x265E;";
            else if (piece.piece == ChessPiece.Knight && piece.color == ChessColor.White)
                code = "&#x2658;";
            else if (piece.piece == ChessPiece.Bishop && piece.color == ChessColor.Black)
                code = "&#x265D;";
            else if (piece.piece == ChessPiece.Bishop && piece.color == ChessColor.White)
                code = "&#x2657;";
            else if (piece.piece == ChessPiece.King && piece.color == ChessColor.Black)
                code = "&#x265A;";
            else if (piece.piece == ChessPiece.King && piece.color == ChessColor.White)
                code = "&#x2654;";
            else if (piece.piece == ChessPiece.Queen && piece.color == ChessColor.Black)
                code = "&#x265B;";
            else if (piece.piece == ChessPiece.Queen && piece.color == ChessColor.White)
                code = "&#x2655;";
            else if (piece.piece == ChessPiece.Pawn && piece.color == ChessColor.Black)
                code = "&#x265F;";
            else if (piece.piece == ChessPiece.Pawn && piece.color == ChessColor.White)
                code = "&#x2659;";
            else
                return null;
            string hexCode = code.Substring(3); // Remove the "&#x" prefix
            List<byte> byteList = new List<byte>();

            for (int i = hexCode.Length - 2; i >= 0; i -= 2)
            {
                string hexByte = hexCode.Substring(i, 2);
                byteList.Add(Convert.ToByte(hexByte, 16));
            }

            byte[] bytes = byteList.ToArray();
            Array.Reverse(bytes); // Reverse the byte array to match the original order
            return Encoding.Unicode.GetString(bytes);
        }
        private void ReverseMove(Notation notation)
        {
            int indexRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(notation.EndField)));
            int indexColumn = chessBoard[indexRow].IndexOf(notation.EndField);
            int indexNewRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(notation.StartField)));
            int indexNewColumn = chessBoard[indexNewRow].IndexOf(notation.StartField);
            string piece = currentChessBoard[indexRow][indexColumn];
            if (!string.IsNullOrEmpty(piece))
            {
                currentChessBoard[indexRow][indexColumn] = PieceToCode(new Piece(notation.Piece, notation.Color));
                currentChessBoard[indexNewRow][indexNewColumn] = piece;
                RefreshBoard();
            }
        }
        private void ReverseMove(Move move)
        {
            int indexRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(move.endField)));
            int indexColumn = chessBoard[indexRow].IndexOf(move.endField);
            int indexNewRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(move.startField)));
            int indexNewColumn = chessBoard[indexNewRow].IndexOf(move.startField);
            string piece = currentChessBoard[indexRow][indexColumn];
            if (!string.IsNullOrEmpty(piece))
            {
                currentChessBoard[indexRow][indexColumn] = "";
                currentChessBoard[indexNewRow][indexNewColumn] = piece;
                RefreshBoard();
            }
        }
        private List<Move> GetMoves(object sender)
        {
            bool pieceInWay = false;
            List<Move> moves = new List<Move>();
            int indexRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(selectedField)));
            int indexColumn = chessBoard[indexRow].IndexOf(selectedField);
            int indexOldRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(selectedField)));
            int indexOldColumn = chessBoard[indexOldRow].IndexOf(selectedField);
            Piece piece = IdentifyChessPiece(sender);
            switch (piece.piece)
            {
                case (ChessPiece.Pawn):
                    switch (piece.color)
                    {
                        case ChessColor.White:
                            if (indexRow + 1 < currentChessBoard.Count && currentChessBoard[indexRow + 1][indexColumn] == "")
                            {
                                moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn]));
                                if (indexRow == 1 && currentChessBoard[indexRow + 2][indexColumn] == "")
                                    moves.Add(new Move(selectedField, chessBoard[indexRow + 2][indexColumn]));
                            }
                            if(indexRow + 1 < currentChessBoard.Count && indexColumn + 1 < currentChessBoard.Count)
                                if(currentChessBoard[indexRow + 1][indexColumn + 1] != "" && IdentifyChessPiece(currentChessBoard[indexRow+1][indexColumn+1]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                                moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn + 1]));
                            if(indexRow + 1 < currentChessBoard.Count && indexColumn - 1 >= 0)
                                if(currentChessBoard[indexRow + 1][indexColumn - 1] != "" && IdentifyChessPiece(currentChessBoard[indexRow+1][indexColumn-1]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                                moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn - 1]));
                            break;
                        case ChessColor.Black:
                            if (indexRow - 1 >= 0 && currentChessBoard[indexRow - 1][indexColumn] == "")
                            {
                                moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn]));
                                if (indexRow == 6 && currentChessBoard[indexRow - 2][indexColumn] == "")
                                    moves.Add(new Move(selectedField, chessBoard[indexRow - 2][indexColumn]));
                            }
                            if (indexRow - 1 >= 0 && indexColumn + 1 < currentChessBoard.Count)
                                if (currentChessBoard[indexRow - 1][indexColumn + 1] != "" && IdentifyChessPiece(currentChessBoard[indexRow-1][indexColumn+1]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                                {
                                    moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn + 1]));
                                    pieceInWay = true;
                                }
                            if (indexRow - 1 >= 0 && indexColumn - 1 >= 0)
                                if (currentChessBoard[indexRow - 1][indexColumn - 1] != "" && IdentifyChessPiece(currentChessBoard[indexRow-1][indexColumn-1]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                                {
                                    moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn - 1]));
                                    pieceInWay = true;
                                }
                            break;
                    }
                    break;
                case ChessPiece.Rook:
                    for(int i = indexRow+1; i < currentChessBoard.Count && !pieceInWay; i++)
                    {
                        if (currentChessBoard[i][indexColumn] == "")
                            moves.Add(new Move(selectedField, chessBoard[i][indexColumn]));
                        else if(IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]) != null)
                            if(IdentifyChessPiece(currentChessBoard[i][indexColumn]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            {
                                moves.Add(new Move(selectedField, chessBoard[i][indexColumn]));
                                pieceInWay = true;
                            }
                            else pieceInWay = true;
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for(int i = indexColumn+1; i < currentChessBoard.Count && !pieceInWay; i++)
                    {
                        if (currentChessBoard[indexRow][i] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow][i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]) != null)
                            if (IdentifyChessPiece(currentChessBoard[indexRow][i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            {
                                moves.Add(new Move(selectedField, chessBoard[indexRow][i]));
                                pieceInWay = true;
                            }
                            else pieceInWay = true;
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for(int i = indexRow-1; i >= 0 && !pieceInWay; i--)
                    {
                        if (currentChessBoard[i][indexColumn] == "")
                            moves.Add(new Move(selectedField, chessBoard[i][indexColumn]));
                        else if (IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]) != null)
                            if (IdentifyChessPiece(currentChessBoard[i][indexColumn]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            {
                                moves.Add(new Move(selectedField, chessBoard[i][indexColumn]));
                                pieceInWay = true;
                            }
                            else pieceInWay = true;
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for(int i = indexColumn-1; i >= 0 && !pieceInWay; i--)
                    {
                        if (currentChessBoard[indexRow][i] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow][i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]) != null)
                            if (IdentifyChessPiece(currentChessBoard[indexRow][i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            {
                                moves.Add(new Move(selectedField, chessBoard[indexRow][i]));
                                pieceInWay = true;
                            }
                            else pieceInWay = true;
                        else
                            pieceInWay = true;
                    }
                    break;
                case ChessPiece.Bishop:
                    for(int i = 1; indexRow+i < currentChessBoard.Count && indexColumn + i < currentChessBoard.Count && !pieceInWay;i++)
                    {
                        if (currentChessBoard[indexRow + i][indexColumn + i] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow + i][indexColumn + i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexRow + i][indexColumn + i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                        {
                            moves.Add(new Move(selectedField, chessBoard[indexRow + i][indexColumn + i]));
                            pieceInWay = true;
                        }
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for(int i = 1; indexRow-i >= 0 && indexColumn - i >= 0 && !pieceInWay; i++)
                    {
                        if (currentChessBoard[indexRow - i][indexColumn - i] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow - i][indexColumn - i]));
                        else if(IdentifyChessPiece(currentChessBoard[indexRow - i][indexColumn - i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                        {
                            moves.Add(new Move(selectedField, chessBoard[indexRow - i][indexColumn - i]));
                            pieceInWay = true;
                        }
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for(int i = 1; indexRow+i < currentChessBoard.Count && indexColumn - i >= 0 && !pieceInWay; i++)
                    {
                        if (currentChessBoard[indexRow + i][indexColumn - i] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow + i][indexColumn - i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexRow + i][indexColumn - i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                        {
                            moves.Add(new Move(selectedField, chessBoard[indexRow + i][indexColumn - i]));
                            pieceInWay = true;
                        }
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for(int i = 1; indexRow-i >= 0 && indexColumn + i < currentChessBoard.Count && !pieceInWay; i++)
                    {
                        if (currentChessBoard[indexRow - i][indexColumn + i] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow - i][indexColumn + i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexRow - i][indexColumn + i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                        {
                            moves.Add(new Move(selectedField, chessBoard[indexRow - i][indexColumn + i]));
                            pieceInWay = true;
                        }
                        else
                            pieceInWay = true;
                    }
                    break;
                case ChessPiece.Knight:
                    if(indexRow + 2 < currentChessBoard.Count && indexColumn + 1 < currentChessBoard.Count && currentChessBoard[indexRow + 2][indexColumn + 1] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow + 2][indexColumn + 1]));
                    else if(indexRow + 2 < currentChessBoard.Count && indexColumn + 1 < currentChessBoard.Count && IdentifyChessPiece(currentChessBoard[indexRow + 2][indexColumn + 1]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            moves.Add(new Move(selectedField, chessBoard[indexRow + 2][indexColumn + 1]));
                    if (indexRow + 2 < currentChessBoard.Count && indexColumn - 1 >= 0 && currentChessBoard[indexRow + 2][indexColumn - 1] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow + 2][indexColumn - 1]));
                    else if(indexRow + 2 < currentChessBoard.Count && indexColumn - 1 >= 0 && IdentifyChessPiece(currentChessBoard[indexRow + 2][indexColumn - 1]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            moves.Add(new Move(selectedField, chessBoard[indexRow + 2][indexColumn - 1]));
                    if (indexRow - 2 >= 0 && indexColumn + 1 < currentChessBoard.Count && currentChessBoard[indexRow - 2][indexColumn + 1] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow - 2][indexColumn + 1]));
                    else if(indexRow - 2 >= 0 && indexColumn + 1 < currentChessBoard.Count && IdentifyChessPiece(currentChessBoard[indexRow - 2][indexColumn + 1]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            moves.Add(new Move(selectedField, chessBoard[indexRow - 2][indexColumn + 1]));
                    if (indexRow - 2 >= 0 && indexColumn - 1 >= 0 && currentChessBoard[indexRow - 2][indexColumn - 1] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow - 2][indexColumn - 1]));
                    else if(indexRow - 2 >= 0 && indexColumn - 1 >= 0 && IdentifyChessPiece(currentChessBoard[indexRow - 2][indexColumn - 1]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            moves.Add(new Move(selectedField, chessBoard[indexRow - 2][indexColumn - 1]));
                    if (indexRow + 1 < currentChessBoard.Count && indexColumn + 2 < currentChessBoard.Count && currentChessBoard[indexRow + 1][indexColumn + 2] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn + 2]));
                    else if(indexRow + 1 < currentChessBoard.Count && indexColumn + 2 < currentChessBoard.Count && IdentifyChessPiece(currentChessBoard[indexRow + 1][indexColumn + 2]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn + 2]));
                    if (indexRow + 1 < currentChessBoard.Count && indexColumn - 2 >= 0 && currentChessBoard[indexRow + 1][indexColumn - 2] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn - 2]));
                    else if(indexRow + 1 < currentChessBoard.Count && indexColumn - 2 >= 0 && IdentifyChessPiece(currentChessBoard[indexRow + 1][indexColumn - 2]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn - 2]));
                    if (indexRow - 1 >= 0 && indexColumn + 2 < currentChessBoard.Count && currentChessBoard[indexRow - 1][indexColumn + 2] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn + 2]));
                    else if(indexRow - 1 >= 0 && indexColumn + 2 < currentChessBoard.Count && IdentifyChessPiece(currentChessBoard[indexRow - 1][indexColumn + 2]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn + 2]));
                    if (indexRow - 1 >= 0 && indexColumn - 2 >= 0 && currentChessBoard[indexRow - 1][indexColumn - 2] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn - 2]));
                    else if(indexRow - 1 >= 0 && indexColumn - 2 >= 0 && IdentifyChessPiece(currentChessBoard[indexRow - 1][indexColumn - 2]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn - 2]));
                    break;
                case ChessPiece.Queen:
                    for (int i = 1; indexRow + i < currentChessBoard.Count && indexColumn + i < currentChessBoard.Count && !pieceInWay; i++)
                    {
                        if (currentChessBoard[indexRow + i][indexColumn + i] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow + i][indexColumn + i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexRow + i][indexColumn + i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                        {
                            moves.Add(new Move(selectedField, chessBoard[indexRow + i][indexColumn + i]));
                            pieceInWay = true;
                        }
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for (int i = 1; indexRow - i >= 0 && indexColumn - i >= 0 && !pieceInWay; i++)
                    {
                        if (currentChessBoard[indexRow - i][indexColumn - i] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow - i][indexColumn - i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexRow - i][indexColumn - i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                        {
                            moves.Add(new Move(selectedField, chessBoard[indexRow - i][indexColumn - i]));
                            pieceInWay = true;
                        }
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for (int i = 1; indexRow + i < currentChessBoard.Count && indexColumn - i >= 0 && !pieceInWay; i++)
                    {
                        if (currentChessBoard[indexRow + i][indexColumn - i] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow + i][indexColumn - i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexRow + i][indexColumn - i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                        {
                            moves.Add(new Move(selectedField, chessBoard[indexRow + i][indexColumn - i]));
                            pieceInWay = true;
                        }
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for (int i = 1; indexRow - i >= 0 && indexColumn + i < currentChessBoard.Count && !pieceInWay; i++)
                    {
                        if (currentChessBoard[indexRow - i][indexColumn + i] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow - i][indexColumn + i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexRow - i][indexColumn + i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                        {
                            moves.Add(new Move(selectedField, chessBoard[indexRow - i][indexColumn + i]));
                            pieceInWay = true;
                        }
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for (int i = indexRow + 1; i < currentChessBoard.Count && !pieceInWay; i++)
                    {
                        if (currentChessBoard[i][indexColumn] == "")
                            moves.Add(new Move(selectedField, chessBoard[i][indexColumn]));
                        else if (IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]) != null)
                            if (IdentifyChessPiece(currentChessBoard[i][indexColumn]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            {
                                moves.Add(new Move(selectedField, chessBoard[i][indexColumn]));
                                pieceInWay = true;
                            }
                            else pieceInWay = true;
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for (int i = indexColumn + 1; i < currentChessBoard.Count && !pieceInWay; i++)
                    {
                        if (currentChessBoard[indexRow][i] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow][i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]) != null)
                            if (IdentifyChessPiece(currentChessBoard[indexRow][i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            {
                                moves.Add(new Move(selectedField, chessBoard[indexRow][i]));
                                pieceInWay = true;
                            }
                            else pieceInWay = true;
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for (int i = indexRow - 1; i >= 0 && !pieceInWay; i--)
                    {
                        if (currentChessBoard[i][indexColumn] == "")
                            moves.Add(new Move(selectedField, chessBoard[i][indexColumn]));
                        else if (IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]) != null)
                            if (IdentifyChessPiece(currentChessBoard[i][indexColumn]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            {
                                moves.Add(new Move(selectedField, chessBoard[i][indexColumn]));
                                pieceInWay = true;
                            }
                            else pieceInWay = true;
                        else
                            pieceInWay = true;
                    }
                    pieceInWay = false;
                    for (int i = indexColumn - 1; i >= 0 && !pieceInWay; i--)
                    {
                        if (currentChessBoard[indexRow][i] == "")
                            moves.Add(new Move(selectedField, chessBoard[indexRow][i]));
                        else if (IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]) != null)
                            if (IdentifyChessPiece(currentChessBoard[indexRow][i]).color != IdentifyChessPiece(currentChessBoard[indexOldRow][indexOldColumn]).color)
                            {
                                moves.Add(new Move(selectedField, chessBoard[indexRow][i]));
                                pieceInWay = true;
                            }
                            else pieceInWay = true;
                        else
                            pieceInWay = true;
                    }
                    break;
                case ChessPiece.King:
                    if(indexRow + 1 < currentChessBoard.Count && currentChessBoard[indexRow + 1][indexColumn] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn]));
                    else if(indexRow + 1 < currentChessBoard.Count && IdentifyChessPiece(currentChessBoard[indexRow + 1][indexColumn]).color != IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color)
                        moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn]));
                    if (indexColumn + 1 < currentChessBoard.Count && currentChessBoard[indexRow][indexColumn + 1] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow][indexColumn + 1]));
                    else if (indexColumn + 1 < currentChessBoard.Count && IdentifyChessPiece(currentChessBoard[indexRow][indexColumn + 1]).color != IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color)
                        moves.Add(new Move(selectedField, chessBoard[indexRow][indexColumn + 1]));
                    if (indexRow - 1 >= 0 && currentChessBoard[indexRow - 1][indexColumn] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn]));
                    else if (indexRow - 1 >= 0 && IdentifyChessPiece(currentChessBoard[indexRow - 1][indexColumn]).color != IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color)
                        moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn]));
                    if (indexColumn - 1 >= 0 && currentChessBoard[indexRow][indexColumn - 1] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow][indexColumn - 1]));
                    else if (indexColumn - 1 >= 0 && IdentifyChessPiece(currentChessBoard[indexRow][indexColumn - 1]).color != IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color)
                        moves.Add(new Move(selectedField, chessBoard[indexRow][indexColumn - 1]));
                    if (indexRow + 1 < currentChessBoard.Count && indexColumn + 1 < currentChessBoard.Count && currentChessBoard[indexRow + 1][indexColumn + 1] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn + 1]));
                    else if (indexRow + 1 < currentChessBoard.Count && indexColumn + 1 < currentChessBoard.Count && IdentifyChessPiece(currentChessBoard[indexRow + 1][indexColumn + 1]).color != IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color)
                        moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn + 1]));
                    if (indexRow - 1 >= 0 && indexColumn - 1 >= 0 && currentChessBoard[indexRow - 1][indexColumn - 1] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn - 1]));
                    else if (indexRow - 1 >= 0 && indexColumn - 1 >= 0 && IdentifyChessPiece(currentChessBoard[indexRow - 1][indexColumn - 1]).color != IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color)
                        moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn - 1]));
                    if (indexRow + 1 < currentChessBoard.Count && indexColumn - 1 >= 0 && currentChessBoard[indexRow + 1][indexColumn - 1] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn - 1]));
                    else if (indexRow + 1 < currentChessBoard.Count && indexColumn - 1 >= 0 && IdentifyChessPiece(currentChessBoard[indexRow + 1][indexColumn - 1]).color != IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color)
                        moves.Add(new Move(selectedField, chessBoard[indexRow + 1][indexColumn - 1]));
                    if (indexRow - 1 >= 0 && indexColumn + 1 < currentChessBoard.Count && currentChessBoard[indexRow - 1][indexColumn + 1] == "")
                        moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn + 1]));
                    else if (indexRow - 1 >= 0 && indexColumn + 1 < currentChessBoard.Count && IdentifyChessPiece(currentChessBoard[indexRow - 1][indexColumn + 1]).color != IdentifyChessPiece(currentChessBoard[indexRow][indexColumn]).color)
                        moves.Add(new Move(selectedField, chessBoard[indexRow - 1][indexColumn + 1]));
                    break;
            }
            return moves;
        }
        private Piece IdentifyChessPiece(object sender)
        {
            Border border = (Border)sender;
            TextBlock textBlock = (TextBlock)border.Child;
            byte[] bytes = Encoding.Unicode.GetBytes(textBlock.Text);
            string text = "&#x";
            for(int i = bytes.Length-1; i >= 0; i--)
            {
                text += $"{bytes[i]:X2}";
            }
            text += ";";
            if (text == "&#x265C;")
                return new Piece(ChessPiece.Rook, ChessColor.Black);
            else if (text == "&#x2656;")
                return new Piece(ChessPiece.Rook, ChessColor.White);
            else if (text == "&#x265E;")
                return new Piece(ChessPiece.Knight, ChessColor.Black);
            else if (text == "&#x2658;")
                return new Piece(ChessPiece.Knight, ChessColor.White);
            else if (text == "&#x265D;")
                return new Piece(ChessPiece.Bishop, ChessColor.Black);
            else if (text == "&#x2657;")
                return new Piece(ChessPiece.Bishop, ChessColor.White);
            else if (text == "&#x265A;")
                return new Piece(ChessPiece.King, ChessColor.Black);
            else if (text == "&#x2654;")
                return new Piece(ChessPiece.King, ChessColor.White);
            else if (text == "&#x265B;")
                return new Piece(ChessPiece.Queen, ChessColor.Black);
            else if (text == "&#x2655;")
                return new Piece(ChessPiece.Queen, ChessColor.White);
            else if (text == "&#x265F;")
                return new Piece(ChessPiece.Pawn, ChessColor.Black);
            else if (text == "&#x2659;")
                return new Piece(ChessPiece.Pawn, ChessColor.White);
            return null;
        }
        private Piece IdentifyChessPiece(string code)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(code);
            string text = "&#x";
            for (int i = bytes.Length - 1; i >= 0; i--)
            {
                text += $"{bytes[i]:X2}";
            }
            text += ";";
            if (text == "&#x265C;")
                return new Piece(ChessPiece.Rook, ChessColor.Black);
            else if (text == "&#x2656;")
                return new Piece(ChessPiece.Rook, ChessColor.White);
            else if (text == "&#x265E;")
                return new Piece(ChessPiece.Knight, ChessColor.Black);
            else if (text == "&#x2658;")
                return new Piece(ChessPiece.Knight, ChessColor.White);
            else if (text == "&#x265D;")
                return new Piece(ChessPiece.Bishop, ChessColor.Black);
            else if (text == "&#x2657;")
                return new Piece(ChessPiece.Bishop, ChessColor.White);
            else if (text == "&#x265A;")
                return new Piece(ChessPiece.King, ChessColor.Black);
            else if (text == "&#x2654;")
                return new Piece(ChessPiece.King, ChessColor.White);
            else if (text == "&#x265B;")
                return new Piece(ChessPiece.Queen, ChessColor.Black);
            else if (text == "&#x2655;")
                return new Piece(ChessPiece.Queen, ChessColor.White);
            else if (text == "&#x265F;")
                return new Piece(ChessPiece.Pawn, ChessColor.Black);
            else if (text == "&#x2659;")
                return new Piece(ChessPiece.Pawn, ChessColor.White);
            return null;
        }
        private void FillChessBoard()
        {
            chessBoard.Clear();
            chessBoard.Add(new List<string> { "A1", "B1", "C1", "D1", "E1", "F1", "G1", "H1" });
            chessBoard.Add(new List<string> { "A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2" });
            chessBoard.Add(new List<string> { "A3", "B3", "C3", "D3", "E3", "F3", "G3", "H3" });
            chessBoard.Add(new List<string> { "A4", "B4", "C4", "D4", "E4", "F4", "G4", "H4" });
            chessBoard.Add(new List<string> { "A5", "B5", "C5", "D5", "E5", "F5", "G5", "H5" });
            chessBoard.Add(new List<string> { "A6", "B6", "C6", "D6", "E6", "F6", "G6", "H6" });
            chessBoard.Add(new List<string> { "A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7" });
            chessBoard.Add(new List<string> { "A8", "B8", "C8", "D8", "E8", "F8", "G8", "H8" });
        }
        private void FillCurrentChessBoard()
        {
            currentChessBoard.Clear();
            string board = "";
            List<DependencyObject> children = GetAllChildren(ChessBoard);
            for(int i = 0; i < children.Count; i++)
            {
                if (children[i] is Border border)
                {
                    if(border.Child is TextBlock textBlock)
                    {
                        if((i+1) % 8 == 0)
                            board += textBlock.Text + ";";
                        else
                            board += textBlock.Text + ",";
                    }
                }
            }
            for(int i = board.Split(';').Length -1; i >= 0; i--)
            {
                if (board.Split(';')[i].Split(',').Length == 8)
                {
                    currentChessBoard.Add(board.Split(';')[i].Split(',').ToList());
                    originalChessBoard.Add(board.Split(';')[i].Split(',').ToList());
                }
            }
        }
        private List<DependencyObject> GetAllChildren(DependencyObject parent)
        {
            List<DependencyObject> children = new List<DependencyObject>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null)
                {
                    children.Add(child);
                }
            }
            return children;
        }
        private void MoveChessPiece(object sender)
        {
            Border border = (Border)sender;
            TextBlock textBlock = (TextBlock)border.Child;
            int indexRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(selectedField)));
            int indexNewRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(border.Name)));
            int indexColumn = chessBoard[indexRow].IndexOf(selectedField);
            int indexNewColumn = chessBoard[indexNewRow].IndexOf(border.Name);
            string piece = currentChessBoard[indexRow][indexColumn];
            if (!string.IsNullOrEmpty(piece))
            {
                currentChessBoard[indexRow][indexColumn] = "";
                currentChessBoard[indexNewRow][indexNewColumn] = piece;
                RefreshBoard();
            }
        }
        private void MoveChessPiece(Move move)
        {
            int indexRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(move.startField)));
            int indexNewRow = chessBoard.IndexOf(chessBoard.Find(x => x.Contains(move.endField)));
            int indexColumn = chessBoard[indexRow].IndexOf(move.startField);
            int indexNewColumn = chessBoard[indexNewRow].IndexOf(move.endField);
            string piece = currentChessBoard[indexRow][indexColumn];
            if (!string.IsNullOrEmpty(piece))
            {
                currentChessBoard[indexRow][indexColumn] = "";
                currentChessBoard[indexNewRow][indexNewColumn] = piece;
                RefreshBoard();
            }
        }
        private void RefreshBoard()
        {
            List<DependencyObject> chessBoardBorders = GetAllChildren(ChessBoard);
            Border selectedBorder = (Border)chessBoardBorders.Find(DependencyObject => DependencyObject is Border border && border.Name == selectedField);
            selectedBorder.Background = borderColors[selectedField];
            selectedCounter = 0;
            for (int i = 0; i < currentChessBoard.Count; i++)
            {
                for(int j = 0; j < currentChessBoard[i].Count; j++)
                {
                    List<DependencyObject> children = GetAllChildren(ChessBoard);
                    for (int k = 0; k < children.Count; k++)
                    {
                        if (children[k] is Border border)
                        {
                            if(border.Name == chessBoard[i][j])
                            {
                                TextBlock textBlock = (TextBlock)border.Child;
                                textBlock.Text = currentChessBoard[i][j];
                            }
                        }
                    }
                }
            }
        }
        private void Common_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedChessField(sender);
        }
    }
    #region Classes
    class Move
    {
        public string startField;
        public string endField;
        public Move(string startField, string endField)
        {
            this.startField = startField;
            this.endField = endField;
        }
    }
    class Piece
    {
        public ChessPiece piece;
        public ChessColor color;
        public Piece(ChessPiece piece, ChessColor color)
        {
            this.piece = piece;
            this.color = color;
        }
    }
    class Notation
    {
        Dictionary<ChessPiece, string> ChessPieceAbbr = new Dictionary<ChessPiece, string>
        {
            {ChessPiece.Pawn, ""},
            {ChessPiece.Knight, "N"},
            {ChessPiece.Bishop, "B"},
            {ChessPiece.Queen, "Q"},
            {ChessPiece.King, "K"},
            {ChessPiece.Rook, "R"},
        };
        public ChessPiece Piece { get; set; }
        public ChessColor Color { get; set; }
        public string StartField { get; set; }
        public string EndField { get; set; }
        public bool Capture { get; set; }
        public bool Check { get; set; }
        public bool Checkmate {  get; set; }
        public bool ShortCastle {  get; set; }
        public bool LongCastle { get; set; }
        public Notation(ChessPiece piece, string startField, string endField, bool capture, bool check, bool checkmate, bool shortCastle, bool longCastle, ChessColor color)
        {
            Piece = piece;
            StartField = startField;
            EndField = endField;
            Capture = capture;
            Check = check;
            Checkmate = checkmate;
            ShortCastle = shortCastle;
            LongCastle = longCastle;
            Color = color;
        }

        public override string ToString()
        {
            string notation = ChessPieceAbbr[Piece] + ";" + StartField + ";" + EndField + ";";
            if (ShortCastle)
                notation = "0-0";
            else if(LongCastle)
                notation = "0-0-0";
            else if (Capture)
                notation = ChessPieceAbbr[Piece] + ";" + StartField + ";" + "x" + ";" + EndField + ";";
            if (Check && !Checkmate)
                notation += "+";
            if (Checkmate)
                notation += "#";
            notation = notation.Replace(";", "");
            return notation;
        }
    }
    #endregion
}
