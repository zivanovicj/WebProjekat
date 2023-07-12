import { useParams, useNavigate } from 'react-router-dom';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Button from 'react-bootstrap/esm/Button';
import {useEffect, useState} from 'react';
import { GetProduct, UpdateProductPicture, UpdateProduct, DelteProduct } from '../../services/ProductService';

function ModifyProduct(){
    const [validated, setValidated] = useState(false);
    const [validationText, setValidationText] = useState("");
    const [product, setProduct] = useState([]);
    const navigate = useNavigate();
    const {productID} = useParams();

    useEffect(() => {
        const get = async () => {
            await GetProduct(productID).then((response) => {
                setProduct(response.data);
                setValidationText('');
            }).catch((error) => {
                setProduct([]);
                setValidationText('Something went wrong');
            })
        }

        get();
    }, [productID])

    const handleUpdate = async (event) => {
        event.preventDefault();
        event.stopPropagation();
        setValidated(true);
        setValidationText("");

        const data = {
            productName: document.getElementById('productName').value,
            price: parseFloat(document.getElementById('price').value),
            amount: parseInt(document.getElementById('amount').value),
            description: document.getElementById('description').value,
            productID: productID
        }
        if(data.productName === "")
            data.productName = product.productName
        if(isNaN(data.price))
            data.price = product.price
        if(isNaN(data.amount))
            data.amount = product.amount
        if(data.description === "")
            data.description = product.description

        await UpdateProduct(data).then((response) => {
            navigate('/details/' + productID);
        }).catch((error) => {
            setValidationText('Something went wrong')
        })
    }

    const handlePictureUpdate = async() => {
        if(document.querySelector('#file').files.length === 0)
            return;
        var formData = new FormData();
        const image = document.querySelector('#file').files[0];
        formData.append("file", image);

        const data = {
            formData: formData,
            token: localStorage.getItem('token'),
            productID: productID
        }

        await UpdateProductPicture(data).then((response) => {
            navigate('/details/' + productID)
        }).catch((error) => {
            setValidationText(error.response.data)
        })
    }

    const handleDelete = async () => {
        await DelteProduct(productID).then((response) => {
            navigate('/myProducts/' + localStorage.getItem('email'));
        }).catch((error) => {
            setValidationText('Something went wrong')
        })
    }

    return (
        <div style={{margin: "auto",
                    width: "40%",
                    border: "3px",
                    padding: "10px"}}>
            <Form.Group className="mb-3">
                <Form.Label>Select new product picture</Form.Label>
                <br/>
                <input type="file" id="file" name="file"/>
            </Form.Group>
            <Button variant="primary" onClick={handlePictureUpdate}>Update picture</Button>
            <br/>
            <br/>
            <Form onSubmit={handleUpdate} noValidate validated={validated}>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Product name</Form.Label>
                    <Col sm="10">
                        <Form.Control type="text" id="productName" required placeholder={product.productName}/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Price(RSD)</Form.Label>
                    <Col sm="10">
                        <Form.Control type="number" step='any' required id="price" placeholder={product.price}/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Amount</Form.Label>
                    <Col sm="10">
                        <Form.Control type="number" required id="amount" placeholder={product.amount}/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Description</Form.Label>
                    <Col sm="10">
                        <Form.Control type="text" required id="description" placeholder={product.description}/>
                    </Col>
                </Form.Group>
                <Form.Text muted>{validationText}</Form.Text>
                <br/>
                <br/>
                <Button type="submit">Update product</Button>
                <Button variant="primary" onClick={handleDelete}>Delete product</Button>
            </Form>
        </div>
    )
}

export default ModifyProduct;