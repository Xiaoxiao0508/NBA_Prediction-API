--SELECT T.TeamName AS TeamName, T.isFav AS isFav, T.PlayerCount AS PlayerCount, ISNULL(SUM(A.PLUS_MINUS * A.PTS / (A.MINS/A.GP)),0) AS DTRScores
--FROM Team AS T
--FULL JOIN PlayerSelection AS P ON P.TeamName = T.TeamName
--LEFT JOIN allPlayers AS A ON A.Player_key = P.Player_key
--WHERE T.UserId = 3
--GROUP BY T.TeamName, T.isFav, T.PlayerCount

SELECT T.UserId, T.TeamName AS TeamName, T.isFav AS isFav, T.PlayerCount AS PlayerCount, ISNULL(SUM(A.PLUS_MINUS * A.PTS / (A.MINS/A.GP)),0) AS DTRScores
FROM Team AS T
LEFT JOIN PlayerSelection AS P ON P.TeamName = T.TeamName
LEFT JOIN allPlayers AS A ON A.Player_key = P.Player_key
WHERE ((T.UserId = 1 AND T.PlayerCount = 0) OR (T.UserId = 1 AND P.UserId = 1))
GROUP BY T.TeamName, T.isFav, T.PlayerCount, T.UserId;