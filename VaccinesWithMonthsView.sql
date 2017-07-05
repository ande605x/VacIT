CREATE VIEW VaccinesWithMonthsView
	AS SELECT v.VacID, v.VacName, v.VacDescription, m.MonthToTake
	FROM Vaccines v
	INNER JOIN MonthToTakeVaccine m
	ON v.VacID=m.VacID
