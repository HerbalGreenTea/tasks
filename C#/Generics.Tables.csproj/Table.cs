using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Generics.Tables
{
    public class Table<TDate, TDepartment, TProceeds>
    {
        public RowsAndColumns Rows { get; set; }
        public RowsAndColumns Columns { get; set; }
        public Open<TDate, TDepartment, TProceeds> Open { get; set; }

        public Table()
        {
            Open = new Open<TDate, TDepartment, TProceeds>();
        }
    }

    public class Open<TDate, TDepartment, TProceeds>
    {
        public Dictionary<CellIndices<TDate, TDepartment>, TProceeds> Cells { get; set; }

        public Open()
        {
            Cells = new Dictionary<CellIndices<TDate, TDepartment>, TProceeds>();
        }

        public TProceeds this [TDate date, TDepartment department]
        {
            get
            {
                return Cells[new CellIndices<TDate, TDepartment>(date, department)];
            }
            set
            {
                Cells.Add(new CellIndices<TDate, TDepartment>(date, department), value);
            }
        }
    }

    public struct CellIndices<TRow, TColumn>
    {
        public readonly TRow Row;
        public readonly TColumn Column;

        public CellIndices(TRow row, TColumn column)
        {
            Row = row;
            Column = column;
        }
    }

    public class RowsAndColumns
    {
        private int count;

        public int Count()
        {
            return count;
        }

        public void Counter()
        {
            count++;
        }
    }

    

















    //public class Table<TDate, TDepartment, TProceeds> 
    //{
    //    private List<TDate> dates;
    //    private List<TDepartment> departments;

    //    public CalculationRowsAndColumns Rows { get; set; }
    //    public CalculationRowsAndColumns Columns { get; set; }
    //    public Dictionary<CellIndices<TDate, TDepartment>, TProceeds> Cells { get; set; }
    //    public TableCellsOpen<TDate, TDepartment, TProceeds> Open { get; set; }
    //    public TableCellsExisted<TDate, TDepartment, TProceeds> Existed { get; set; }

    //    public Table()
    //    {
    //        Rows = new CalculationRowsAndColumns();
    //        Columns = new CalculationRowsAndColumns();
    //        Cells = new Dictionary<CellIndices<TDate, TDepartment>, TProceeds>();
    //        Open = new TableCellsOpen<TDate, TDepartment, TProceeds>();
    //        Existed = new TableCellsExisted<TDate, TDepartment, TProceeds>();
    //    }

    //    public void AddRow(TDate date)
    //    {
    //        Rows.AddCount();
    //        dates.Add(date);
    //        FillTable();
    //    }

    //    public void AddColumn(TDepartment department)
    //    {
    //        Columns.AddCount();
    //        departments.Add(department);
    //        FillTable();
    //    }

    //    public void FillTable()
    //    {
    //        Cells = Open.Count() <= Existed.Count() ? Open.ReportData() : Existed.ReportData();

    //        var numberWholePairs = dates.Count < departments.Count ? dates.Count : departments.Count;

    //        for (int i = 0; i < numberWholePairs; i++)
    //        {
    //            Cells.Add(new CellIndices<TDate, TDepartment>(dates[i], departments[i]), default(TProceeds));
    //        }

    //        Open.Add(Cells);
    //        Existed.Add(Cells);
    //    }
    //}

    //public struct CellIndices<TRow, TColumn>
    //{
    //    public readonly TRow Row;
    //    public readonly TColumn Column;

    //    public CellIndices(TRow row, TColumn column)
    //    {
    //        Row = row;
    //        Column = column;
    //    }
    //}

    //public class TableCellsOpen<TDate, TDepartment, TProceeds> : TableCells<TDate, TDepartment, TProceeds>
    //{
    //    public TProceeds this [TDate date, TDepartment department]
    //    {
    //        get
    //        {
    //            return Cells[new CellIndices<TDate, TDepartment>(date, department)];
    //        }
    //        set
    //        {
    //            Cells.Add(new CellIndices<TDate, TDepartment>(date, department), value);
    //        }
    //    }
    //}

    //public class TableCellsExisted<TDate, TDepartment, TProceeds> : TableCells<TDate, TDepartment, TProceeds>
    //{
    //    public TProceeds this[TDate date, TDepartment department]
    //    {
    //        get
    //        {
    //            if (Cells.ContainsKey(new CellIndices<TDate, TDepartment>(date, department)))
    //                return Cells[new CellIndices<TDate, TDepartment>(date, department)];

    //            throw new ArgumentException();
    //        }
    //        set
    //        {
    //            if (Cells.ContainsKey(new CellIndices<TDate, TDepartment>(date, department)))
    //                Cells.Add(new CellIndices<TDate, TDepartment>(date, department), value);

    //            throw new ArgumentException();
    //        }
    //    }
    //}

    //public abstract class TableCells<TDate, TDepartment, TProceeds>
    //{
    //    public Dictionary<CellIndices<TDate, TDepartment>, TProceeds> Cells;
    //    public void Add(Dictionary<CellIndices<TDate, TDepartment>, TProceeds> cells)
    //    {
    //        Cells = cells;
    //    }

    //    public Dictionary<CellIndices<TDate, TDepartment>, TProceeds> ReportData()
    //    {
    //        return Cells;
    //    }

    //    public int Count()
    //    {
    //        return Cells.Count();
    //    }
    //}

    //public class CalculationRowsAndColumns
    //{
    //    private int count;

    //    public int Count()
    //    {
    //        return count;
    //    }

    //    public void AddCount()
    //    {
    //        count++;
    //    }
    //}
}
