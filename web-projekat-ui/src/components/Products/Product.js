import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import { CartContext } from '../../store/CartContext';
import React, { useContext } from 'react';

function Product(props){
  const product = props.product
  const modify = props.modify
  const { addToCart } = useContext(CartContext);

    return (
        <Card style={{ width: '18rem' }}>
          <Card.Body>
            <Card.Text style = {{fontWeight: 'bold'}}>{product.productName}</Card.Text>
            <Card.Subtitle className="mb-2 text-muted">{product.price} RSD</Card.Subtitle>
            <Card.Text>{product.description}</Card.Text>
            <Button href={'/details/' + product.productID} variant="primary">Details</Button>
            {localStorage.getItem('userType') === 'CUSTOMER' && 
              <Button variant="primary" onClick={() => addToCart({productID: product.productID, quantity: 1, productName: product.productName})}>Add to cart</Button>}
            {modify && <Button href={'/modifyProduct/' + product.productID} variant='primary'>Modify</Button>}
          </Card.Body>
        </Card>
      );
}

export default Product;