﻿namespace Application
{
    using Framework.Server.Application;
    using System;

    public class ApplicationX : ApplicationBase
    {
        protected override Data DataCreate()
        {
            Data result = new Data();
            result.Session = Guid.NewGuid();
            //
            var container = new LayoutContainer(result, "Container");
            var rowHeader = new LayoutRow(container, "Header");
            var cellHeader1 = new LayoutCell(rowHeader, "HeaderCell1");
            var rowContent = new LayoutRow(container, "Content");
            var cellContent1 = new LayoutCell(rowContent, "ContentCell1");
            var cellContent2 = new LayoutCell(rowContent, "ContentCell2");
            new Label(cellContent2, "Enter text");
            new Input(cellContent2, "MyTest");
            var rowFooter = new LayoutRow(container, "Footer");
            var cellFooter1 = new LayoutCell(rowFooter, "FooterCell1");
            var button = new Button(cellFooter1, "Hello");
            var grid = new Grid(cellFooter1, "Grid");
            grid.Load(typeof(Database.dbo.AirportDisplay));
            //
            return result;
        }
    }
}
