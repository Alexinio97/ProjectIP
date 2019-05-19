<?php
$marca=$_POST['marca'];

$servername = "localhost";
$username = "username";
$password = "password";
$dbname = "myDB";

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);
// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}

$sql = "SELECT Marca, CNP, Prenume, Nume FROM MyGuests where marca='$marca'";
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