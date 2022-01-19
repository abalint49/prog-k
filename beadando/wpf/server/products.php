<?php

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
  if ($_GET['products'] == '0') {
    $stmt = $pdo->prepare('SELECT id,category,name,description,price,stock FROM products');
    $stmt->execute();
    $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
    
    return;
  }
  if ($_GET['products'] == 'id') {
    $stmt = $pdo->prepare('SELECT id FROM products');
    $stmt->execute();
    $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
    return;
  }
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
  $data = json_decode(file_get_contents('php://input'));

  if ($_GET['products'] == '')
  {
    $stmt = $pdo->prepare('INSERT INTO products (category, name, description, price, stock) VALUES (?, ?, ?, ?, 0)');
    $stmt->execute([$data->category, $data->name, $data->description, $data->price]);
    $data->id = $pdo->lastInsertId();
    if (!$data) {
      $data = null;
    }
    return;
  }

  if ($_GET['products'] == 'id')
  {
    $stmt = $pdo->prepare('SELECT category,name,description,price,stock FROM products WHERE id = ?');
    $stmt->execute([$data->id]);
    $data = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$data) {
      $data = null;
    }
    return;
  }

  if ($_GET['products'] == 'delete')
  {
    $stmt = $pdo->prepare('DELETE FROM products WHERE id = ?');
    $stmt->execute([$data->id]);
    $data = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$data) {
      $data = null;
    }
    return;
  }
  if ($_GET['products'] == 'rent')
  {
    $stmt = $pdo->prepare('UPDATE products SET stock = ? WHERE id = ? && stock = 0');
    $stmt->execute([$data->stock, $data->id]);
    $data = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$data) {
      $data = null;
    }
    return;
  }

  if ($_GET['products'] == 'back')
  {
    $stmt = $pdo->prepare('UPDATE products SET stock = 0 WHERE id = ? && stock = ?');
    $stmt->execute([$data->id, $data->stock]);
    $data = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$data) {
      $data = null;
    }
    return;
  }
}