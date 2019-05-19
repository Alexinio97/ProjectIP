<?php
$departament=$_POST['departament'];

$servername = "db4free.net";
$username = "dragonfly97";
$password = "DragonFly123";
$dbname = "dragonfly";

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}

$sql = "SELECT Marca, CNP, Prenume, Nume FROM database_name where departament='$departament'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    // output data of each row
    while($row = mysqli_fetch_assoc($result)) {
        echo "Marca: " . $row["Marca"]." CNP: ". $row["CNP"]. " Nume: " . $row["Prenume"]. " " . $row["Nume"]. "<br>";
    }
} else {
    echo "0 results";
}

mysqli_close($conn);
?>