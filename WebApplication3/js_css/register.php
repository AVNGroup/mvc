<?php
$connect mysql_connect('localhost','root','') or die(mysql_error());
mysql_select_db('puppy_care_db');

if(isset($_POST['submit'])){
    $username = $_Post['username'];
    $surname = $_Post['surname'];
    $petname = $_Post['petname'];
    $login = $_Post['login'];
    $password = $_Post['password'];
    $r_password = $_Post['r_password'];
    if($password == $r_password){
        $password = md5($password);
        $query=mysql_query("INSERT INTO users VALUES('$username','$surname','$petname','$login','$password')") or die(mysql_error());
    }
    else{
        die("Passwords must match!")
    }
    }
?>


    <form method="post" action="register.php">
        <input type="text" name="username" placeholder="|Username" required/>
        <br>
        <input type="text" name="surname" placeholder="|surname" required/>
        <br>
        <input type="text" name="petname" placeholder="|petname" required/>
        <br>
        <input type="text" name="login" placeholder="|login" required/>
        <br>
        <input type="text" name="password" placeholder="|password" required/>
        <br>
        <input type="text" name="r_password" placeholder="|repeat password" required/>
        <br>

    </form>