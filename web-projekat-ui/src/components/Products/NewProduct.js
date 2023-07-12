import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Button from 'react-bootstrap/esm/Button';
import { useState } from 'react';
import { AddProduct } from '../../services/ProductService';
import { useNavigate } from 'react-router-dom';

function NewProduct(){
    const [validated, setValidated] = useState(false);
    const [validationText, setValidationText] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async (event) => {
        event.preventDefault();
        const form = event.currentTarget;
        if (form.checkValidity() === false){
            event.preventDefault();
            event.stopPropagation();
            setValidationText("Please enter all fields");
            return;
        }

        setValidated(true);
        setValidationText("");

        const data = {
            productName: document.getElementById('productName').value,
            price: parseFloat(document.getElementById('price').value),
            amount: parseInt(document.getElementById('amount').value),
            description: document.getElementById('description').value,
        }
        if(document.querySelector('#file').files.length !== 0){
            const image = document.querySelector('#file').files[0];
            data['file'] = image
        }

        console.log(data);

        await AddProduct(data).then((response) => {
            navigate('/myProducts/' + localStorage.getItem('email'))
        }).catch((error) => {
            setValidationText('Something went wrong')
        })
    }

    return (
        <div>
            <Form onSubmit={handleSubmit} noValidate validated={validated} className="w-50 mt-5 mx-auto col-10 col-md-8 col-lg-6">
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Product name</Form.Label>
                    <Col sm="10">
                        <Form.Control type="text" id="productName" required/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Price(RSD)</Form.Label>
                    <Col sm="10">
                        <Form.Control type="number" step='any' required id="price"/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Amount</Form.Label>
                    <Col sm="10">
                        <Form.Control type="number" required id="amount"/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Description</Form.Label>
                    <Col sm="10">
                        <Form.Control type="text" required id="description"/>
                    </Col>
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Select product image</Form.Label>
                    <br/>
                    <input type="file" id="file" name="file"/>
                </Form.Group>
                <Form.Text id="validation" muted>{validationText}</Form.Text>
                <br/>
                <br/>
                <Button type="submit">Add product</Button>
            </Form>
        </div>
    )
}

export default NewProduct;