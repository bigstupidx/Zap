<?php

    $serverName = "localhost";
    $username = "root";
    $password = "";
    $dbName = "zap_db";

    // Make connection
    $conn = new mysqli($serverName, $username, $password, $dbName);
    // Check connection
    if(!$conn) {
        die("Connection Failed" . mysqli_connect_error());
    }
	
	// get request type
	$request=($_POST['request']);
	
	// authenticate user
	if($request == 'AUTHENTICATE_USER')
	{
		// Get parameters from get request
		$email=($_POST['email']);
		$pass=($_POST['password']);
		// Create Query
		$sql = "SELECT * FROM users WHERE email = '".$email."' AND password = '".$pass."'";
		$result = mysqli_query($conn, $sql);

		if($result && mysqli_num_rows($result) > 0 ) {
			echo "AUTHORIZED";
		}
		else {
			echo 'UNAUTHORIZED';
		}
	}
	else if($request == 'CREATE_USER')
	{
		// Get parameters from get request
		$user=($_POST['username']);
		$pass=($_POST['password']);
		$email=($_POST['email']);
		
		// See if email already exists
		$sql = "SELECT * FROM users WHERE email = '".$email."'";
		$result = mysqli_query($conn, $sql);
		if(mysqli_num_rows($result) > 0 ) {
			echo 'ALREADY_EXISTS';
		}
		else {
			// add user
			$sql = "INSERT INTO users (username, password, email) VALUES ('".$user."', '".$pass."', '".$email."')";
			$result = mysqli_query($conn, $sql);
			if ($result) {
				echo "AUTHORIZED";
			} else {
				echo 'UNAUTHORIZED';
			}
		}
	}
	else if($request == 'SET_HIGHSCORE')
	{
		$email=($_POST['email']);
		$score=($_POST['score']);
		
		$sql = "SELECT highscore FROM highscores WHERE email = '".$email."'";
		$result = mysqli_query($conn, $sql);
		$row = mysqli_fetch_assoc($result);
		$success = false;
		
		// check to see if we got initial highscore back from database
		if($result && mysqli_num_rows($result) > 0) {
			// make sure new highscore is greater than current in database
			if($score > $row['highscore']) {
				$sql = "UPDATE highscores SET highscore='".$score."' WHERE email='".$email."'";
				$result = mysqli_query($conn, $sql);
				if ($result) {
					$success = true; // success score is higher and set
				}
			}
		}
		else
		{
			// insert into database since it doesn't exist in database currently
			$sql = "INSERT INTO highscores (email, highscore) VALUES('".$email."', '".$score."')";
			$result = mysqli_query($conn, $sql);
			if ($result) {
				$success = true;
			} else {
				$success = false;
			}
		}
		
		// send back success or failure response
		if ($success) {
			echo "SUCCESS";
		} else {
			echo "FAILED";
		}
	}
	else if($request == 'GET_HIGHSCORE')
	{
		
	}
	
?>