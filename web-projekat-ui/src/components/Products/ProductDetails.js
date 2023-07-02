import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { useEffect } from 'react';
import { GetProduct } from '../../services/ProductService';
import { useParams } from 'react-router-dom';
import { useState } from 'react';

function ProductDetails(){
    const {id} = useParams();
    const [product, setProduct] = useState([]);
    const loggedIn = localStorage.getItem("token");
  
    useEffect(()=>{
        const get = async() => {
        const response = await GetProduct(id);
        console.log(response.data);
        setProduct(response.data);
        }
        get();
    }, [id])

    return (
        <div style={{margin: "auto",
            width: "20%",
            border: "3px",
            padding: "10px"}}>
        <Card style={{ width: '18rem' }}>
          <Card.Img variant="top" src={product.image} />
          <Card.Body>
            <Card.Title>{product.productName}</Card.Title>
            <Card.Subtitle className="mb-2 text-muted">{product.price} RSD</Card.Subtitle>
            <Card.Text>{product.description}</Card.Text>
            {loggedIn !== null && <Button variant="primary">Add to cart</Button>}
          </Card.Body>
        </Card>
        </div>
      );
}

export default ProductDetails;