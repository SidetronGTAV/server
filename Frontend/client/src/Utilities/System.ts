/* eslint-disable @typescript-eslint/no-explicit-any,@typescript-eslint/no-unused-vars */
export class System {
     private static valueOf = Symbol.prototype.valueOf;

     public static cloneDeep(val: any, instanceClone: any = null): any {
          switch (System.typeOf(val)) {
               case 'object':
                    return System.cloneObjectDeep(val, instanceClone);
               case 'array':
                    return System.cloneArrayDeep(val, instanceClone);
               default: {
                    return System.clone(val);
               }
          }
     }

     private static cloneObjectDeep(val: any, instanceClone: any): any {
          // @ts-ignore
          if (typeof instanceClone === 'private static') {
               // @ts-ignore
               return instanceClone(val);
          }
          if (instanceClone || System.isPlainObject(val)) {
               const res = new val.constructor();
               for (const key in val) {
                    res[key] = System.cloneDeep(val[key], instanceClone);
               }
               return res;
          }
          return val;
     }

     private static cloneArrayDeep(val: any, instanceClone: any): any {
          const res = new val.constructor(val.length);
          for (let i = 0; i < val.length; i++) {
               res[i] = System.cloneDeep(val[i], instanceClone);
          }
          return res;
     }

     private static isPlainObject(o: any): any {
          if (!System.isObject(o)) return false;

          // If has modified constructor
          const ctor = o.constructor;
          if (ctor === undefined) return true;

          // If has modified prototype
          const prot = ctor.prototype;
          if (!System.isObject(prot)) return false;

          // If constructor does not have an Object-specific method
          if (!prot.hasOwnProperty('isPrototypeOf')) {
               return false;
          }

          // Most likely a plain Object
          return true;
     }

     private static isObject(o: any): boolean {
          return Object.prototype.toString.call(o) === '[object Object]';
     }

     private static clone(val: any): any {
          switch (System.typeOf(val)) {
               case 'array':
                    return val.slice();
               case 'object':
                    return Object.assign({}, val);
               case 'date':
                    return new val.constructor(Number(val));
               case 'map':
                    return new Map(val);
               case 'set':
                    return new Set(val);
               case 'buffer':
                    return System.cloneBuffer(val);
               case 'symbol':
                    return System.cloneSymbol(val);
               case 'arraybuffer':
                    return System.cloneArrayBuffer(val);
               case 'float32array':
               case 'float64array':
               case 'int16array':
               case 'int32array':
               case 'int8array':
               case 'uint16array':
               case 'uint32array':
               case 'uint8clampedarray':
               case 'uint8array':
                    return System.cloneTypedArray(val);
               case 'regexp':
                    return System.cloneRegExp(val);
               case 'error':
                    return Object.create(val);
               default: {
                    return val;
               }
          }
     }

     private static cloneRegExp(val: any): any {
          const flags = val.flags !== void 0 ? val.flags : /\w+$/.exec(val) || void 0;
          const re = new val.constructor(val.source, flags);
          re.lastIndex = val.lastIndex;
          return re;
     }

     private static cloneArrayBuffer(val: any): any {
          const res = new val.constructor(val.byteLength);
          new Uint8Array(res).set(new Uint8Array(val));
          return res;
     }

     private static cloneTypedArray(val: any): any {
          return new val.constructor(val.buffer, val.byteOffset, val.length);
     }

     private static cloneBuffer(val: any): any {
          const len = val.length;
          // @ts-ignore
          const buf = Buffer.allocUnsafe ? Buffer.allocUnsafe(len) : Buffer.from(len);
          val.copy(buf);
          return buf;
     }

     private static cloneSymbol(val: any): any {
          return System.valueOf ? Object(System.valueOf.call(val)) : {};
     }

     private static typeOf(val: any): any {
          if (val === void 0) return 'undefined';
          if (val === null) return 'null';

          let type: any = typeof val;
          if (type === 'boolean') return 'boolean';
          if (type === 'string') return 'string';
          if (type === 'number') return 'number';
          if (type === 'symbol') return 'symbol';
          if (type === 'function') {
               return System.isGeneratorFn(val) ? 'generatorfunction' : 'function';
          }

          if (System.isArray(val)) return 'array';
          if (System.isBuffer(val)) return 'buffer';
          if (System.isArguments(val)) return 'arguments';
          if (System.isDate(val)) return 'date';
          if (System.isError(val)) return 'error';
          if (System.isRegexp(val)) return 'regexp';

          switch (System.ctorName(val)) {
               case 'Symbol':
                    return 'symbol';
               case 'Promise':
                    return 'promise';

               // Set, Map, WeakSet, WeakMap
               case 'WeakMap':
                    return 'weakmap';
               case 'WeakSet':
                    return 'weakset';
               case 'Map':
                    return 'map';
               case 'Set':
                    return 'set';

               // 8-bit typed arrays
               case 'Int8Array':
                    return 'int8array';
               case 'Uint8Array':
                    return 'uint8array';
               case 'Uint8ClampedArray':
                    return 'uint8clampedarray';

               // 16-bit typed arrays
               case 'Int16Array':
                    return 'int16array';
               case 'Uint16Array':
                    return 'uint16array';

               // 32-bit typed arrays
               case 'Int32Array':
                    return 'int32array';
               case 'Uint32Array':
                    return 'uint32array';
               case 'Float32Array':
                    return 'float32array';
               case 'Float64Array':
                    return 'float64array';
          }

          if (System.isGeneratorObj(val)) {
               return 'generator';
          }

          // Non-plain objects
          // @ts-ignore
          type = toString.call(val);
          // @ts-ignore
          switch (type) {
               case '[object Object]':
                    return 'object';
               // iterators
               case '[object Map Iterator]':
                    return 'mapiterator';
               case '[object Set Iterator]':
                    return 'setiterator';
               case '[object String Iterator]':
                    return 'stringiterator';
               case '[object Array Iterator]':
                    return 'arrayiterator';
          }

          // other
          return type.slice(8, -1).toLowerCase().replace(/\s/g, '');
     }

     private static ctorName(val: any): any {
          return typeof val.constructor === 'function' ? val.constructor.name : null;
     }

     private static isArray(val: any): any {
          if (Array.isArray) return Array.isArray(val);
          return val instanceof Array;
     }

     private static isError(val: any): any {
          return val instanceof Error || (typeof val.message === 'string' && val.constructor && typeof val.constructor.stackTraceLimit === 'number');
     }

     private static isDate(val: any): any {
          if (val instanceof Date) return true;
          return typeof val.toDateString === 'function' && typeof val.getDate === 'function' && typeof val.setDate === 'function';
     }

     private static isRegexp(val: any): any {
          if (val instanceof RegExp) return true;
          return typeof val.flags === 'string' && typeof val.ignoreCase === 'boolean' && typeof val.multiline === 'boolean' && typeof val.global === 'boolean';
     }

     private static isGeneratorFn(name: any): any {
          return System.ctorName(name) === 'GeneratorFunction';
     }

     private static isGeneratorObj(val: any): any {
          return typeof val.throw === 'function' && typeof val.return === 'function' && typeof val.next === 'function';
     }

     private static isArguments(val: any): any {
          try {
               if (typeof val.length === 'number' && typeof val.callee === 'function') {
                    return true;
               }
          } catch (err: any) {
               if (err.message.indexOf('callee') !== -1) {
                    return true;
               }
          }
          return false;
     }

     /**
      * If you need to support Safari 5-7 (8-10 yr-old browser),
      * take a look at https://github.com/feross/is-buffer
      */

     private static isBuffer(val: any): any {
          if (val.constructor && typeof val.constructor.isBuffer === 'function') {
               return val.constructor.isBuffer(val);
          }
          return false;
     }
}
