package main

import (
	"fmt"
	"math"
	"math/rand"
	"runtime"
	"time"
)

// Node representa um nó na árvore
type Node struct {
	ID       int
	Children []*Node
}

// generateTree gera uma árvore com o número especificado de nós
func generateTree(nodeCount int) {
	root := &Node{ID: 1}
	rand.Seed(time.Now().UnixNano())
	generateNode(root, 1, int(math.Log2(float64(nodeCount))))
}

// generateNode gera recursivamente os nós da árvore
func generateNode(parent *Node, depth, remainingNodes int) {
	if remainingNodes <= 0 || depth <= 0 {
		return
	}

	childrenCount := 2
	if remainingNodes < 2 {
		childrenCount = remainingNodes
	}

	for i := 0; i < childrenCount; i++ {
		child := &Node{ID: rand.Intn(math.MaxInt32)}
		parent.Children = append(parent.Children, child)
		generateNode(child, depth-1, remainingNodes/childrenCount)
		remainingNodes -= remainingNodes / childrenCount
	}
}

// measureMemory retorna a quantidade de memória usada pelo processo
func measureMemory() uint64 {
	var m runtime.MemStats
	runtime.ReadMemStats(&m)
	return m.Alloc
}

func main() {
	// Medir o uso de memória inicial
	initialMemory := measureMemory()

	// Medir o tempo de execução
	start := time.Now()

	nodeCount := 1000000
	generateTree(nodeCount)

	// Medir o tempo de execução
	elapsed := time.Since(start)

	// Medir o uso de memória final
	finalMemory := measureMemory()

	// Calcular a diferença de memória
	memoryUsage := finalMemory - initialMemory

	// Imprimir resultados
	fmt.Printf("Execution time: %d milliseconds\n", elapsed.Nanoseconds())
	fmt.Printf("Memory usage: %d bytes\n", memoryUsage)
}
